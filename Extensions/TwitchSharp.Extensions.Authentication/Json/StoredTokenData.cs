using System.Text.Json.Serialization;

namespace TwitchSharp.Extensions.Authentication.Json;

/// <summary>
/// Internal DTO for persisting token data to a JSON file.
/// Maps to and from <see cref="TwitchSharp.Api.Authentication.TwitchTokenSet"/>.
/// </summary>
internal sealed record StoredTokenData
{
    /// <summary>The OAuth2 access token.</summary>
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }

    /// <summary>The OAuth2 refresh token, if available.</summary>
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; init; }

    /// <summary>The UTC time at which the access token expires, if known.</summary>
    [JsonPropertyName("expires_at_utc")]
    public DateTimeOffset? ExpiresAtUtc { get; init; }

    /// <summary>The token type (typically "bearer").</summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; } = "bearer";

    /// <summary>The scopes granted for this token.</summary>
    [JsonPropertyName("scopes")]
    public string[] Scopes { get; init; } = [];

    /// <summary>The raw OIDC ID token JWT, if the openid scope was requested.</summary>
    [JsonPropertyName("id_token")]
    public string? IdToken { get; init; }
}
