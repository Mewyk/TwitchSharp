using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a blocked user returned by the Get User Block List endpoint.
/// </summary>
public sealed record UserBlockData
{
    /// <summary>The blocked user's ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The blocked user's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The blocked user's display name.</summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; init; } = string.Empty;
}
