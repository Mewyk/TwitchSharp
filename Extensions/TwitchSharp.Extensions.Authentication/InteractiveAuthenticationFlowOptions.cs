namespace TwitchSharp.Extensions.Authentication;

/// <summary>
/// Configuration options for <see cref="InteractiveAuthenticationFlow"/>.
/// </summary>
public sealed class InteractiveAuthenticationFlowOptions
{
    /// <summary>
    /// The Twitch application Client ID. Required for building the authorization URL.
    /// This should be the same Client ID used to configure the <see cref="TwitchSharp.Api.TwitchApiClient"/>.
    /// </summary>
    public required string ClientId { get; init; }

    /// <summary>
    /// The redirect URI where the OAuth callback will be received.
    /// Must be registered with the Twitch application and must be a localhost URI
    /// (e.g., <c>http://localhost:3000/callback/</c>).
    /// </summary>
    public required string RedirectUri { get; init; }

    /// <summary>
    /// Optional configuration for the local OAuth callback listener.
    /// Allows customizing the HTML pages served after the callback.
    /// </summary>
    public OAuthCallbackListenerOptions? CallbackListenerOptions { get; set; }

    /// <summary>
    /// If <c>true</c>, forces the user to re-authorize the application even if they have previously authorized it.
    /// Default is <c>false</c>.
    /// </summary>
    public bool ForceVerify { get; set; }

    /// <summary>
    /// Optional file path for automatic token persistence.
    /// If set, tokens are saved to this file after successful authentication and loaded
    /// on subsequent calls. When a valid (non-expired) cached token is found, the browser
    /// flow is skipped entirely.
    /// </summary>
    public string? TokenFilePath { get; set; }
}
