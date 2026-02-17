using System.Net.Http.Headers;
using System.Text.Json;
using TwitchSharp.Api.Authentication;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch OAuth2 token lifecycle operations including validation, revocation,
/// authorization code exchange, token refresh, and device code flow.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class AuthenticationClient
{
    private readonly TwitchApiClientOptions _options;
    private readonly Func<HttpClient> _oauthClientFactory;

    internal AuthenticationClient(TwitchApiClientOptions options, Func<HttpClient> oauthClientFactory)
    {
        _options = options;
        _oauthClientFactory = oauthClientFactory;
    }

    /// <summary>
    /// Validates an access token by calling the Twitch token validation endpoint.
    /// </summary>
    /// <param name="accessToken">The access token to validate.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>Token validation data including client ID, scopes, and expiration.</returns>
    /// <exception cref="TwitchApiException">Thrown when the token validation response is empty.</exception>
    public async Task<TokenValidationData> ValidateTokenAsync(
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(accessToken);

        var client = _oauthClientFactory();

        using var request = new HttpRequestMessage(HttpMethod.Get, "validate");
        request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", accessToken);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response;
        try
        {
            response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (HttpRequestException exception)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, "oauth2/validate");
        }
        catch (TaskCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, "oauth2/validate");
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
                    // Ignore content reading errors
                }
                catch (IOException)
                {
                    // Ignore content reading errors
                }

                throw TwitchErrorMapper.FromHttpResponse(response.StatusCode, "oauth2/validate", errorMessage);
            }

            TokenValidationData? result;
            try
            {
                await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
                result = await JsonSerializer.DeserializeAsync(
                    stream,
                    TwitchApiJsonContext.Default.TokenValidationData,
                    cancellationToken).ConfigureAwait(false);
            }
            catch (JsonException exception)
            {
                throw TwitchErrorMapper.FromDeserializationError(exception, "oauth2/validate");
            }

            if (result is null)
            {
                throw new TwitchApiException(
                    TwitchErrorCodes.DeserializationError,
                    "Token validation response was empty.",
                    endpoint: "oauth2/validate");
            }

            return result;
        }
    }

    /// <summary>
    /// Revokes an access token. Works for both user access tokens and app access tokens.
    /// </summary>
    /// <param name="accessToken">The access token to revoke.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task RevokeTokenAsync(
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(accessToken);

        var client = _oauthClientFactory();

        var parameters = new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["token"] = accessToken
        };

        using var content = new FormUrlEncodedContent(parameters);
        using var request = new HttpRequestMessage(HttpMethod.Post, "revoke")
        {
            Content = content
        };

        HttpResponseMessage response;
        try
        {
            response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (HttpRequestException exception)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, "oauth2/revoke");
        }
        catch (TaskCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, "oauth2/revoke");
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
                    // Ignore content reading errors
                }
                catch (IOException)
                {
                    // Ignore content reading errors
                }

                throw TwitchErrorMapper.FromHttpResponse(response.StatusCode, "oauth2/revoke", errorMessage);
            }
        }
    }

    /// <summary>
    /// Exchanges an authorization code for an access token and refresh token.
    /// This is step 2 of the Authorization Code Grant flow.
    /// </summary>
    /// <param name="code">The authorization code received from the redirect.</param>
    /// <param name="redirectUri">The redirect URI that was used in the authorization request.</param>
    /// <param name="codeVerifier">Optional PKCE code verifier. Required if a code challenge was used in the authorization request.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A token set containing the access token, refresh token, and metadata.</returns>
    /// <exception cref="TwitchApiException">Thrown when the client secret is not configured.</exception>
    public async Task<TwitchTokenSet> ExchangeCodeAsync(
        string code,
        string redirectUri,
        string? codeVerifier = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(code);
        ArgumentNullException.ThrowIfNull(redirectUri);

        if (string.IsNullOrEmpty(_options.ClientSecret))
        {
            throw new TwitchApiException(
                TwitchErrorCodes.Unauthorized,
                "ClientSecret is required for authorization code exchange.");
        }

        var client = _oauthClientFactory();

        var parameters = new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["client_secret"] = _options.ClientSecret,
            ["code"] = code,
            ["grant_type"] = "authorization_code",
            ["redirect_uri"] = redirectUri
        };

        if (codeVerifier is not null)
        {
            parameters["code_verifier"] = codeVerifier;
        }

        using var content = new FormUrlEncodedContent(parameters);
        return await TwitchTokenManager.SendTokenRequestAsync(client, content, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Refreshes a user access token using a refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token to use.</param>
    /// <param name="scopes">Optional subset of scopes to request. If null, the originally granted scopes are preserved.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A new token set with fresh access and refresh tokens.</returns>
    /// <exception cref="TwitchApiException">Thrown when the client secret is not configured.</exception>
    public async Task<TwitchTokenSet> RefreshTokenAsync(
        string refreshToken,
        IEnumerable<string>? scopes = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(refreshToken);

        if (string.IsNullOrEmpty(_options.ClientSecret))
        {
            throw new TwitchApiException(
                TwitchErrorCodes.Unauthorized,
                "ClientSecret is required for token refresh.");
        }

        var client = _oauthClientFactory();

        var parameters = new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["client_secret"] = _options.ClientSecret,
            ["grant_type"] = "refresh_token",
            ["refresh_token"] = refreshToken
        };

        if (scopes is not null)
        {
            var scopeValue = string.Join(' ', scopes);
            if (scopeValue.Length > 0)
            {
                parameters["scope"] = scopeValue;
            }
        }

        using var content = new FormUrlEncodedContent(parameters);
        return await TwitchTokenManager.SendTokenRequestAsync(client, content, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Starts the device code authorization flow by requesting a device code and user code.
    /// The user must navigate to the returned verification URI and enter the user code.
    /// </summary>
    /// <param name="scopes">The OAuth scopes to request.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>Device code data including the user code and verification URI.</returns>
    /// <exception cref="TwitchApiException">Thrown when the device code response is empty.</exception>
    public async Task<DeviceCodeData> StartDeviceFlowAsync(
        string[] scopes,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(scopes);

        var client = _oauthClientFactory();

        var parameters = new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["scopes"] = string.Join(' ', scopes)
        };

        using var content = new FormUrlEncodedContent(parameters);
        using var request = new HttpRequestMessage(HttpMethod.Post, "device")
        {
            Content = content
        };
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response;
        try
        {
            response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (HttpRequestException exception)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, "oauth2/device");
        }
        catch (TaskCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, "oauth2/device");
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
                    // Ignore content reading errors
                }
                catch (IOException)
                {
                    // Ignore content reading errors
                }

                throw TwitchErrorMapper.FromHttpResponse(response.StatusCode, "oauth2/device", errorMessage);
            }

            DeviceCodeData? result;
            try
            {
                await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
                result = await JsonSerializer.DeserializeAsync(
                    stream,
                    TwitchApiJsonContext.Default.DeviceCodeData,
                    cancellationToken).ConfigureAwait(false);
            }
            catch (JsonException exception)
            {
                throw TwitchErrorMapper.FromDeserializationError(exception, "oauth2/device");
            }

            if (result is null)
            {
                throw new TwitchApiException(
                    TwitchErrorCodes.DeserializationError,
                    "Device code response was empty.",
                    endpoint: "oauth2/device");
            }

            return result;
        }
    }

    /// <summary>
    /// Polls the token endpoint for device code flow completion.
    /// Returns a token set if the user has authorized, or throws if still pending or expired.
    /// </summary>
    /// <param name="deviceCode">The device code from <see cref="StartDeviceFlowAsync"/>.</param>
    /// <param name="scopes">The same scopes used in <see cref="StartDeviceFlowAsync"/>.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A token set if authorization is complete.</returns>
    /// <exception cref="TwitchApiException">Thrown with <see cref="TwitchErrorCodes.BadRequest"/>
    /// if the user has not yet authorized (authorization_pending) or the device code has expired.</exception>
    public async Task<TwitchTokenSet> PollDeviceFlowAsync(
        string deviceCode,
        string[] scopes,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(deviceCode);
        ArgumentNullException.ThrowIfNull(scopes);

        var client = _oauthClientFactory();

        var parameters = new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["scopes"] = string.Join(' ', scopes),
            ["device_code"] = deviceCode,
            ["grant_type"] = "urn:ietf:params:oauth:grant-type:device_code"
        };

        using var content = new FormUrlEncodedContent(parameters);
        return await TwitchTokenManager.SendTokenRequestAsync(client, content, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the authenticated user's OIDC profile from the Twitch UserInfo endpoint.
    /// Requires the <c>openid</c> scope.
    /// </summary>
    /// <param name="accessToken">A valid access token obtained with the <c>openid</c> scope.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The user's OIDC profile data.</returns>
    /// <exception cref="TwitchApiException">Thrown when the user info response is empty.</exception>
    public async Task<UserInfoData> GetUserInfoAsync(
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(accessToken);

        var client = _oauthClientFactory();

        using var request = new HttpRequestMessage(HttpMethod.Get, "userinfo");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response;
        try
        {
            response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (HttpRequestException exception)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, "oauth2/userinfo");
        }
        catch (TaskCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, "oauth2/userinfo");
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
                    // Ignore content reading errors
                }
                catch (IOException)
                {
                    // Ignore content reading errors
                }

                throw TwitchErrorMapper.FromHttpResponse(response.StatusCode, "oauth2/userinfo", errorMessage);
            }

            UserInfoData? result;
            try
            {
                await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
                result = await JsonSerializer.DeserializeAsync(
                    stream,
                    TwitchApiJsonContext.Default.UserInfoData,
                    cancellationToken).ConfigureAwait(false);
            }
            catch (JsonException exception)
            {
                throw TwitchErrorMapper.FromDeserializationError(exception, "oauth2/userinfo");
            }

            if (result is null)
            {
                throw new TwitchApiException(
                    TwitchErrorCodes.DeserializationError,
                    "UserInfo response was empty.",
                    endpoint: "oauth2/userinfo");
            }

            return result;
        }
    }
}
