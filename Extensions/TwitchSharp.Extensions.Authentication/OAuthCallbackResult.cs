namespace TwitchSharp.Extensions.Authentication;

/// <summary>
/// Represents the result of an OAuth callback received by <see cref="OAuthCallbackListener"/>.
/// </summary>
public sealed record OAuthCallbackResult
{
    /// <summary>
    /// The authorization code received from the OAuth callback.
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// The state parameter returned from the OAuth callback, if present.
    /// Should be validated against the state sent in the authorization request for CSRF protection.
    /// </summary>
    public string? State { get; init; }
}
