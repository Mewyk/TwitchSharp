using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a broadcaster's Shield Mode status.
/// </summary>
public sealed record ShieldModeData
{
    /// <summary>Whether Shield Mode is active.</summary>
    [JsonPropertyName("is_active")]
    public bool IsActive { get; init; }

    /// <summary>The ID of the moderator that last activated Shield Mode.</summary>
    [JsonPropertyName("moderator_id")]
    public string ModeratorId { get; init; } = string.Empty;

    /// <summary>The moderator's login name.</summary>
    [JsonPropertyName("moderator_login")]
    public string ModeratorLogin { get; init; } = string.Empty;

    /// <summary>The moderator's display name.</summary>
    [JsonPropertyName("moderator_name")]
    public string ModeratorName { get; init; } = string.Empty;

    /// <summary>When Shield Mode was last activated (RFC3339).</summary>
    [JsonPropertyName("last_activated_at")]
    public string LastActivatedAt { get; init; } = string.Empty;
}
