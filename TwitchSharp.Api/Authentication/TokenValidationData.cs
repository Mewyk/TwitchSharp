using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Authentication;

/// <summary>
/// Represents the response from the Twitch token validation endpoint (GET /oauth2/validate).
/// </summary>
public sealed record TokenValidationData
{
    /// <summary>The client ID associated with the validated token.</summary>
    [JsonPropertyName("client_id")]
    public string ClientId { get; init; } = string.Empty;

    /// <summary>The user's login name. Empty for app access tokens.</summary>
    [JsonPropertyName("login")]
    public string Login { get; init; } = string.Empty;

    /// <summary>The scopes granted to the validated token.</summary>
    [JsonPropertyName("scopes")]
    public string[] Scopes { get; init; } = [];

    /// <summary>The user's ID. Empty for app access tokens.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The number of seconds until the token expires.</summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }
}
