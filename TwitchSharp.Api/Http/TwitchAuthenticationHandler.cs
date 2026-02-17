using TwitchSharp.Api.Authentication;

namespace TwitchSharp.Api.Http;

/// <summary>
/// Specifies the authentication mode for a Twitch API request.
/// </summary>
internal enum TwitchAuthenticationMode
{
    /// <summary>No authentication.</summary>
    None,

    /// <summary>Use an app access token (client credentials flow).</summary>
    AppToken,

    /// <summary>Use a user access token (authorization code flow).</summary>
    UserToken
}

/// <summary>
/// Keys for passing request-level options via <see cref="HttpRequestMessage.Options"/>.
/// </summary>
internal static class TwitchRequestOptions
{
    /// <summary>
    /// The authentication mode to use for the request.
    /// </summary>
    public static readonly HttpRequestOptionsKey<TwitchAuthenticationMode> AuthenticationMode = new("TwitchSharp.AuthenticationMode");
}

/// <summary>
/// A <see cref="DelegatingHandler"/> that injects Twitch authentication headers
/// (<c>Client-Id</c> and <c>Authorization: Bearer</c>) into outgoing requests.
/// </summary>
internal sealed class TwitchAuthenticationHandler : DelegatingHandler
{
    private readonly TwitchApiClientOptions _options;
    private readonly TwitchTokenManager _tokenManager;
    private readonly Func<HttpClient> _oauthClientFactory;

    public TwitchAuthenticationHandler(
        TwitchApiClientOptions options,
        TwitchTokenManager tokenManager,
        Func<HttpClient> oauthClientFactory)
    {
        _options = options;
        _tokenManager = tokenManager;
        _oauthClientFactory = oauthClientFactory;
    }

    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var authenticationMode = request.Options.TryGetValue(TwitchRequestOptions.AuthenticationMode, out var mode)
            ? mode
            : TwitchAuthenticationMode.AppToken;

        // Always add Client-Id
        request.Headers.TryAddWithoutValidation("Client-Id", _options.ClientId);

        if (authenticationMode is not TwitchAuthenticationMode.None)
        {
            var accessToken = authenticationMode switch
            {
                TwitchAuthenticationMode.AppToken => await _tokenManager.GetAppTokenAsync(_oauthClientFactory, cancellationToken).ConfigureAwait(false),
                TwitchAuthenticationMode.UserToken => await _tokenManager.GetUserTokenAsync(_oauthClientFactory, cancellationToken).ConfigureAwait(false),
                _ => throw new InvalidOperationException($"Unknown authentication mode: {authenticationMode}")
            };

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        }

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
