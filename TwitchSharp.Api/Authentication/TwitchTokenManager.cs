using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Authentication;

/// <summary>
/// Manages OAuth token acquisition (client credentials) and refresh (authorization code) for Twitch API access.
/// Thread-safe for concurrent use.
/// </summary>
internal sealed partial class TwitchTokenManager : IAsyncDisposable
{
    private readonly TwitchApiClientOptions _options;
    private readonly TokenManagerOptions _tokenManagerOptions;
    private readonly ILogger _logger;
    private readonly SemaphoreSlim _appTokenGate = new(1, 1);
    private readonly SemaphoreSlim _userTokenGate = new(1, 1);

    private TwitchTokenSet? _appToken;
    private TwitchTokenSet? _userToken;

    /// <summary>
    /// Initializes a new instance of the <see cref="TwitchTokenManager"/> class.
    /// </summary>
    /// <param name="options">The API client options containing credentials and configuration.</param>
    /// <param name="tokenManagerOptions">Optional token management behavior options.</param>
    /// <param name="loggerFactory">An optional logger factory for diagnostic logging.</param>
    public TwitchTokenManager(TwitchApiClientOptions options, TokenManagerOptions? tokenManagerOptions = null, ILoggerFactory? loggerFactory = null)
    {
        _options = options;
        _tokenManagerOptions = tokenManagerOptions ?? new TokenManagerOptions();
        _logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<TwitchTokenManager>();
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Acquired app access token (expires: {ExpiresAt})")]
    private partial void LogAppTokenAcquired(DateTimeOffset? expiresAt);

    [LoggerMessage(Level = LogLevel.Information, Message = "User token expired, refreshing")]
    private partial void LogUserTokenRefreshStarted();

    [LoggerMessage(Level = LogLevel.Information, Message = "User token refreshed (expires: {ExpiresAt})")]
    private partial void LogUserTokenRefreshed(DateTimeOffset? expiresAt);

    [LoggerMessage(Level = LogLevel.Error, Message = "Token persistence callback failed")]
    private partial void LogTokenPersistenceFailed(Exception exception);

    /// <summary>
    /// Sets the user token for user-authenticated requests.
    /// </summary>
    /// <param name="tokenSet">The token set containing the user access token and optional refresh token.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="tokenSet"/> is <c>null</c>.</exception>
    public void SetUserToken(TwitchTokenSet tokenSet)
    {
        ArgumentNullException.ThrowIfNull(tokenSet);
        Interlocked.Exchange(ref _userToken, tokenSet);
    }

    /// <summary>
    /// Gets a valid app access token, acquiring or refreshing as needed.
    /// </summary>
    /// <param name="oauthClientFactory">A factory that creates an <see cref="HttpClient"/> configured for the Twitch OAuth endpoint.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A valid app access token string.</returns>
    /// <exception cref="TwitchApiException">Thrown when the token cannot be acquired.</exception>
    public async ValueTask<string> GetAppTokenAsync(
        Func<HttpClient> oauthClientFactory,
        CancellationToken cancellationToken)
    {
        var current = Volatile.Read(ref _appToken);
        if (current is not null && !current.IsExpired(DateTimeOffset.UtcNow, _options.TokenExpirationBuffer))
        {
            return current.AccessToken;
        }

        await _appTokenGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            // Double-check after acquiring the gate
            current = _appToken;
            if (current is not null && !current.IsExpired(DateTimeOffset.UtcNow, _options.TokenExpirationBuffer))
            {
                return current.AccessToken;
            }

            var newToken = await AcquireAppTokenAsync(oauthClientFactory, cancellationToken).ConfigureAwait(false);
            Volatile.Write(ref _appToken, newToken);
            LogAppTokenAcquired(newToken.ExpiresAtUtc);
            return newToken.AccessToken;
        }
        finally
        {
            _appTokenGate.Release();
        }
    }

    /// <summary>
    /// Gets a valid user access token, refreshing if needed.
    /// </summary>
    /// <param name="oauthClientFactory">A factory that creates an <see cref="HttpClient"/> configured for the Twitch OAuth endpoint.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A valid user access token string.</returns>
    /// <exception cref="TwitchApiException">Thrown when no user token has been set or the token cannot be refreshed.</exception>
    public async ValueTask<string> GetUserTokenAsync(
        Func<HttpClient> oauthClientFactory,
        CancellationToken cancellationToken)
    {
        var current = Volatile.Read(ref _userToken);
        if (current is null)
        {
            throw new TwitchApiException(
                TwitchErrorCodes.Unauthorized,
                "No user token has been set. Call SetUserToken() before making user-authenticated requests.");
        }

        if (!current.IsExpired(DateTimeOffset.UtcNow, _options.TokenExpirationBuffer))
        {
            return current.AccessToken;
        }

        if (string.IsNullOrEmpty(current.RefreshToken))
        {
            throw new TwitchApiException(
                TwitchErrorCodes.Unauthorized,
                "User token is expired and no refresh token is available.");
        }

        await _userTokenGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            // Double-check after acquiring the gate
            current = _userToken;
            if (current is not null && !current.IsExpired(DateTimeOffset.UtcNow, _options.TokenExpirationBuffer))
            {
                return current.AccessToken;
            }

            if (current?.RefreshToken is not { Length: > 0 } refreshToken)
            {
                throw new TwitchApiException(
                    TwitchErrorCodes.Unauthorized,
                    "User token is expired and no refresh token is available.");
            }

            LogUserTokenRefreshStarted();
            var newToken = await RefreshUserTokenAsync(oauthClientFactory, refreshToken, cancellationToken).ConfigureAwait(false);

            if (_tokenManagerOptions.OnTokenRefreshed is { } callback)
            {
                try
                {
                    await callback(newToken, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception exception) when (exception is not OperationCanceledException)
                {
                    LogTokenPersistenceFailed(exception);
                    throw TwitchErrorMapper.FromTokenPersistenceFailed(exception);
                }
            }

            Volatile.Write(ref _userToken, newToken);
            LogUserTokenRefreshed(newToken.ExpiresAtUtc);
            return newToken.AccessToken;
        }
        finally
        {
            _userTokenGate.Release();
        }
    }

    private async Task<TwitchTokenSet> AcquireAppTokenAsync(
        Func<HttpClient> oauthClientFactory,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(_options.ClientSecret))
        {
            throw new TwitchApiException(
                TwitchErrorCodes.Unauthorized,
                "ClientSecret is required for client credentials flow (app tokens).");
        }

        using var client = oauthClientFactory();

        var parameters = new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["client_secret"] = _options.ClientSecret,
            ["grant_type"] = "client_credentials"
        };

        using var content = new FormUrlEncodedContent(parameters);
        return await SendTokenRequestAsync(client, content, cancellationToken).ConfigureAwait(false);
    }

    private async Task<TwitchTokenSet> RefreshUserTokenAsync(
        Func<HttpClient> oauthClientFactory,
        string refreshToken,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(_options.ClientSecret))
        {
            throw new TwitchApiException(
                TwitchErrorCodes.Unauthorized,
                "ClientSecret is required for token refresh.");
        }

        using var client = oauthClientFactory();

        var parameters = new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["client_secret"] = _options.ClientSecret,
            ["grant_type"] = "refresh_token",
            ["refresh_token"] = refreshToken
        };

        using var content = new FormUrlEncodedContent(parameters);
        return await SendTokenRequestAsync(client, content, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a token request to the Twitch OAuth endpoint and parses the response.
    /// </summary>
    /// <param name="client">The HTTP client configured for the Twitch OAuth endpoint.</param>
    /// <param name="content">The form-encoded request content containing OAuth parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="TwitchTokenSet"/> containing the acquired token information.</returns>
    /// <exception cref="TwitchApiException">Thrown when the token request fails or the response cannot be parsed.</exception>
    internal static async Task<TwitchTokenSet> SendTokenRequestAsync(
        HttpClient client,
        HttpContent content,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "token")
        {
            Content = content
        };
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response;
        try
        {
            response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
        }
        catch (HttpRequestException exception)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, "oauth2/token");
        }
        catch (TaskCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, "oauth2/token");
        }

        using (response)
        {
            if (!response.IsSuccessStatusCode)
            {
                string? errorMessage = null;
                try
                {
                    errorMessage = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                    if (errorMessage is { Length: > 200 })
                    {
                        errorMessage = string.Concat(errorMessage.AsSpan(0, 200), "...");
                    }
                }
                catch (HttpRequestException)
                {
                    // Ignore content reading errors during error response processing
                }
                catch (IOException)
                {
                    // Ignore content reading errors during error response processing
                }

                throw TwitchErrorMapper.FromHttpResponse(response.StatusCode, "oauth2/token", errorMessage);
            }

            OAuthTokenResponse? tokenResponse;
            try
            {
                await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
                tokenResponse = await JsonSerializer.DeserializeAsync(
                    stream,
                    TwitchApiJsonContext.Default.OAuthTokenResponse,
                    cancellationToken).ConfigureAwait(false);
            }
            catch (JsonException exception)
            {
                throw TwitchErrorMapper.FromDeserializationError(exception, "oauth2/token");
            }

            if (tokenResponse?.AccessToken is null)
            {
                throw new TwitchApiException(
                    TwitchErrorCodes.DeserializationError,
                    "OAuth token response did not contain an access token.",
                    endpoint: "oauth2/token");
            }

            return new TwitchTokenSet
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken,
                ExpiresAtUtc = tokenResponse.ExpiresIn > 0
                    ? DateTimeOffset.UtcNow.AddSeconds(tokenResponse.ExpiresIn)
                    : null,
                TokenType = tokenResponse.TokenType ?? "bearer",
                Scopes = tokenResponse.Scope ?? [],
                IdToken = tokenResponse.IdToken
            };
        }
    }

    /// <inheritdoc />
    public ValueTask DisposeAsync()
    {
        _appTokenGate.Dispose();
        _userTokenGate.Dispose();
        return ValueTask.CompletedTask;
    }
}
