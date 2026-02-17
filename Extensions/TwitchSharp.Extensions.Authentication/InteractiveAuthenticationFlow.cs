using TwitchSharp.Api;
using TwitchSharp.Api.Authentication;

namespace TwitchSharp.Extensions.Authentication;

/// <summary>
/// Orchestrates a complete OAuth Authorization Code + PKCE flow for interactive authentication.
/// Handles PKCE generation, authorization URL construction, browser launching, local callback listening,
/// and token exchange in a single <see cref="AuthenticateAsync"/> call.
/// </summary>
/// <remarks>
/// This is intended for desktop, console, and CLI applications that need to authenticate a user
/// interactively via the browser. For web applications, the framework typically handles
/// the OAuth redirect; use <see cref="TwitchApiClient.ExchangeCodeAndSetUserTokenAsync"/> directly instead.
/// </remarks>
public sealed class InteractiveAuthenticationFlow
{
    private readonly InteractiveAuthenticationFlowOptions _options;

    /// <summary>
    /// Creates a new <see cref="InteractiveAuthenticationFlow"/> with the specified options.
    /// </summary>
    /// <param name="options">The flow configuration options.</param>
    public InteractiveAuthenticationFlow(InteractiveAuthenticationFlowOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        _options = options;
    }

    /// <summary>
    /// Runs the complete Authorization Code + PKCE flow.
    /// </summary>
    /// <remarks>
    /// <para>When <see cref="InteractiveAuthenticationFlowOptions.TokenFilePath"/> is set and a valid cached
    /// token exists, the browser flow is skipped and the cached token is returned directly.</para>
    /// <para>Otherwise, this method:
    /// <list type="number">
    /// <item>Generates a PKCE code verifier and challenge.</item>
    /// <item>Generates a cryptographic state parameter for CSRF protection.</item>
    /// <item>Builds the Twitch authorization URL.</item>
    /// <item>Opens the URL in the system browser.</item>
    /// <item>Starts a local HTTP listener for the callback.</item>
    /// <item>Waits for the user to authorize and be redirected back.</item>
    /// <item>Validates the state parameter.</item>
    /// <item>Exchanges the authorization code for tokens.</item>
    /// <item>Optionally saves the tokens to disk.</item>
    /// </list></para>
    /// </remarks>
    /// <param name="client">The <see cref="TwitchApiClient"/> to use for the token exchange.</param>
    /// <param name="scopes">The OAuth scopes to request.</param>
    /// <param name="cancellationToken">A cancellation token to abort the flow.</param>
    /// <returns>The acquired <see cref="TwitchTokenSet"/> ready for use with <see cref="TwitchApiClient.SetUserToken"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the state parameter in the callback does not match the expected value.</exception>
    public async Task<TwitchTokenSet> AuthenticateAsync(
        TwitchApiClient client,
        string[] scopes,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(scopes);

        // Try to load a cached token if persistence is configured
        FileTokenStore? tokenStore = null;
        if (_options.TokenFilePath is not null)
        {
            tokenStore = new FileTokenStore(_options.TokenFilePath);
            var cachedToken = await tokenStore.LoadAsync(cancellationToken).ConfigureAwait(false);

            if (cachedToken is not null && !cachedToken.IsExpired(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(60)))
            {
                return cachedToken;
            }
        }

        ArgumentNullException.ThrowIfNull(client);

        // Generate PKCE pair
        var (codeVerifier, codeChallenge) = PkceChallenge.Generate();

        // Generate a state parameter for CSRF protection
        var expectedState = StateGenerator.Generate();

        // Build the authorization URL
        var authorizationUrl = TwitchAuthorizationUrlBuilder.Create()
            .WithClientId(_options.ClientId)
            .WithRedirectUri(_options.RedirectUri)
            .ForAuthorizationCode()
            .WithScopes(scopes)
            .WithState(expectedState)
            .WithPkce(codeChallenge);

        if (_options.ForceVerify)
        {
            authorizationUrl.ForceVerify();
        }

        var authorizationUri = authorizationUrl.Build();

        // Start the local callback listener before opening the browser
        using var callbackListener = new OAuthCallbackListener(_options.RedirectUri, _options.CallbackListenerOptions);

        // Open the browser
        SystemBrowser.Open(authorizationUri);

        // Wait for the callback
        var callbackResult = await callbackListener.ListenForCallbackAsync(cancellationToken).ConfigureAwait(false);

        // Validate the state parameter
        if (callbackResult.State != expectedState)
        {
            throw new InvalidOperationException(
                "The state parameter in the OAuth callback does not match the expected value. " +
                "This may indicate a CSRF attack or a stale authorization response.");
        }

        // Exchange the code for tokens
        var tokenSet = await client.ExchangeCodeAndSetUserTokenAsync(
            callbackResult.Code,
            _options.RedirectUri,
            codeVerifier,
            cancellationToken).ConfigureAwait(false);

        // Save the tokens if persistence is configured
        if (tokenStore is not null)
        {
            await tokenStore.SaveAsync(tokenSet, cancellationToken).ConfigureAwait(false);
        }

        return tokenSet;
    }
}
