using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a user's chat color returned by the Get User Chat Color endpoint.
/// </summary>
public sealed record ChatColorData
{
    /// <summary>The user's ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The user's chat color as a hex string (e.g., "#FF0000").</summary>
    [JsonPropertyName("color")]
    public string Color { get; init; } = string.Empty;
}
