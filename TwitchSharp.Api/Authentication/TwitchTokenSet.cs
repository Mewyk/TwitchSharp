namespace TwitchSharp.Api.Authentication;

/// <summary>
/// Represents a set of OAuth tokens for Twitch API authentication.
/// </summary>
public sealed record TwitchTokenSet
{
    /// <summary>
    /// The OAuth2 access token.
    /// </summary>
    public required string AccessToken { get; init; }

    /// <summary>
    /// The OAuth2 refresh token, if available (authorization code flow only).
    /// </summary>
    public string? RefreshToken { get; init; }

    /// <summary>
    /// The UTC time at which the access token expires, if known.
    /// </summary>
    public DateTimeOffset? ExpiresAtUtc { get; init; }

    /// <summary>
    /// The token type (typically "bearer").
    /// </summary>
    public string TokenType { get; init; } = "bearer";

    /// <summary>
    /// The scopes granted for this token.
    /// </summary>
    public IReadOnlyList<string> Scopes { get; init; } = [];

    /// <summary>
    /// The raw OIDC ID token JWT, if the <c>openid</c> scope was requested.
    /// Use <see cref="OidcTokenParser.ParseIdToken"/> to extract claims.
    /// </summary>
    public string? IdToken { get; init; }

    /// <summary>
    /// Determines whether the token is expired or will expire within the given buffer.
    /// </summary>
    /// <param name="currentTime">The current UTC time.</param>
    /// <param name="buffer">A buffer period before the actual expiration to consider the token expired.</param>
    /// <returns><c>true</c> if the token is expired or will expire within the buffer; otherwise <c>false</c>.</returns>
    public bool IsExpired(DateTimeOffset currentTime, TimeSpan buffer)
    {
        if (ExpiresAtUtc is not { } expiresAt)
        {
            return false;
        }

        return currentTime >= expiresAt - buffer;
    }
}
