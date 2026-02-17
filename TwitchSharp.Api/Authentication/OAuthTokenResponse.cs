using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Authentication;

/// <summary>
/// Internal DTO for deserializing OAuth2 token endpoint responses from Twitch.
/// </summary>
internal sealed record OAuthTokenResponse
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; init; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; init; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }

    [JsonPropertyName("scope")]
    public string[]? Scope { get; init; }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; init; }

    [JsonPropertyName("id_token")]
    public string? IdToken { get; init; }
}
