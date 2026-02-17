using Microsoft.Extensions.Logging;
using TwitchSharp.Api.Authentication;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.RateLimiting;

namespace TwitchSharp.Api;

/// <summary>
/// The main entry point for the Twitch Helix API.
/// </summary>
/// <remarks>
/// This client is safe for concurrent use and should be reused for the lifetime of the application.
/// Category client properties (Channels, Chat, Streams, etc.) will be added in Phase 2 via partial class files.
/// </remarks>
public sealed partial class TwitchApiClient : IAsyncDisposable
{
    private readonly HelixHttpClient _httpClient;
    private readonly TwitchApiClientOptions _options;
    private readonly Func<HttpClient>? _oauthClientFactory;
    private readonly HttpClient? _ownedHelixClient;
    private readonly HttpClient? _ownedOAuthClient;
    private readonly TwitchTokenManager _tokenManager;

    private TwitchApiClient(
        HelixHttpClient httpClient,
        TwitchTokenManager tokenManager,
        TwitchApiClientOptions options,
        Func<HttpClient>? oauthClientFactory,
        HttpClient? ownedHelixClient,
        HttpClient? ownedOAuthClient)
    {
        _httpClient = httpClient;
        _tokenManager = tokenManager;
        _options = options;
        _oauthClientFactory = oauthClientFactory;
        _ownedHelixClient = ownedHelixClient;
        _ownedOAuthClient = ownedOAuthClient;
    }

    /// <summary>
    /// Creates a new <see cref="TwitchApiClient"/> for standalone usage without dependency injection.
    /// </summary>
    /// <param name="options">The API client options.</param>
    /// <param name="tokenManagerOptions">Optional token manager options for persistence callbacks.</param>
    /// <param name="loggerFactory">Optional logger factory for diagnostic logging.</param>
    /// <returns>A new <see cref="TwitchApiClient"/> that owns its HTTP clients.</returns>
    /// <remarks>
    /// This method creates and owns internal HttpClient instances.
    /// The client should be disposed when no longer needed.
    /// For long-running apps, reuse the same instance rather than creating one per request.
    /// </remarks>
    public static TwitchApiClient Create(
        TwitchApiClientOptions options,
        TokenManagerOptions? tokenManagerOptions = null,
        ILoggerFactory? loggerFactory = null)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (string.IsNullOrWhiteSpace(options.ClientId))
        {
            throw new ArgumentException("ClientId is required.", nameof(options));
        }

        var rateLimiter = new TwitchRateLimiter(options, loggerFactory);
        var tokenManager = new TwitchTokenManager(options, tokenManagerOptions, loggerFactory);

        var oauthClient = new HttpClient
        {
            BaseAddress = TwitchEndpoints.OAuthBaseUri,
            Timeout = options.RequestTimeout
        };

        // Build handler pipeline: TwitchAuthenticationHandler -> TwitchResilienceHandler -> HttpClientHandler
        var innerHandler = new HttpClientHandler();

        HttpMessageHandler pipeline;
        if (options.DisableBuiltInResilience)
        {
            var authenticationHandler = new TwitchAuthenticationHandler(options, tokenManager, CreateOAuthClientFactory)
            {
                InnerHandler = innerHandler
            };
            pipeline = authenticationHandler;
        }
        else
        {
            var resilienceHandler = new TwitchResilienceHandler(options, loggerFactory)
            {
                InnerHandler = innerHandler
            };
            var authenticationHandler = new TwitchAuthenticationHandler(options, tokenManager, CreateOAuthClientFactory)
            {
                InnerHandler = resilienceHandler
            };
            pipeline = authenticationHandler;
        }

        var helixClient = new HttpClient(pipeline, disposeHandler: true)
        {
            BaseAddress = TwitchEndpoints.HelixBaseUri,
            Timeout = options.RequestTimeout
        };

        var httpClient = new HelixHttpClient(
            () => helixClient,
            rateLimiter,
            loggerFactory);

        var client = new TwitchApiClient(
            httpClient,
            tokenManager,
            options,
            CreateOAuthClientFactory,
            ownedHelixClient: helixClient,
            ownedOAuthClient: oauthClient);

        return client;

        // Local function to avoid closure allocation on the hot path
        HttpClient CreateOAuthClientFactory() => oauthClient;
    }

    /// <summary>
    /// Creates a new <see cref="TwitchApiClient"/> with externally provided HttpClient factories.
    /// </summary>
    /// <param name="options">The API client options.</param>
    /// <param name="helixClientFactory">Factory to obtain the Helix HttpClient.</param>
    /// <param name="oauthClientFactory">Factory to obtain the OAuth HttpClient.</param>
    /// <param name="tokenManagerOptions">Optional token manager options for persistence callbacks.</param>
    /// <param name="loggerFactory">Optional logger factory for diagnostic logging.</param>
    /// <returns>A new <see cref="TwitchApiClient"/> that does not own the HttpClient instances.</returns>
    public static TwitchApiClient Create(
        TwitchApiClientOptions options,
        Func<HttpClient> helixClientFactory,
        Func<HttpClient> oauthClientFactory,
        TokenManagerOptions? tokenManagerOptions = null,
        ILoggerFactory? loggerFactory = null)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(helixClientFactory);
        ArgumentNullException.ThrowIfNull(oauthClientFactory);

        if (string.IsNullOrWhiteSpace(options.ClientId))
        {
            throw new ArgumentException("ClientId is required.", nameof(options));
        }

        var rateLimiter = new TwitchRateLimiter(options, loggerFactory);
        var tokenManager = new TwitchTokenManager(options, tokenManagerOptions, loggerFactory);

        var httpClient = new HelixHttpClient(
            helixClientFactory,
            rateLimiter,
            loggerFactory);

        return new TwitchApiClient(
            httpClient,
            tokenManager,
            options,
            oauthClientFactory,
            ownedHelixClient: null,
            ownedOAuthClient: null);
    }

    /// <summary>
    /// Creates a new <see cref="TwitchApiClient"/> with pre-built components. Intended for DI registration.
    /// </summary>
    internal static TwitchApiClient CreateFromComponents(
        HelixHttpClient httpClient,
        TwitchTokenManager tokenManager,
        TwitchApiClientOptions options,
        Func<HttpClient> oauthClientFactory)
    {
        return new TwitchApiClient(
            httpClient,
            tokenManager,
            options,
            oauthClientFactory,
            ownedHelixClient: null,
            ownedOAuthClient: null);
    }

    /// <summary>
    /// Sets the user token for user-authenticated requests.
    /// </summary>
    /// <param name="tokenSet">The user token set containing at minimum an access token.</param>
    public void SetUserToken(TwitchTokenSet tokenSet)
    {
        _tokenManager.SetUserToken(tokenSet);
    }

    /// <summary>
    /// Gets the internal HTTP client for use by category clients. Intended for internal use only.
    /// </summary>
    internal HelixHttpClient HttpClient => _httpClient;

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        await _httpClient.DisposeAsync().ConfigureAwait(false);
        await _tokenManager.DisposeAsync().ConfigureAwait(false);

        _ownedHelixClient?.Dispose();
        _ownedOAuthClient?.Dispose();
    }
}
