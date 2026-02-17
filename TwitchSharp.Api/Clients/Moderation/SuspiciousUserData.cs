using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a suspicious user status.
/// </summary>
public sealed record SuspiciousUserData
{
    /// <summary>The ID of the user with the suspicious status.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's ID.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The ID of the moderator who applied the last status.</summary>
    [JsonPropertyName("moderator_id")]
    public string ModeratorId { get; init; } = string.Empty;

    /// <summary>When this user's status was last updated.</summary>
    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; init; } = string.Empty;

    /// <summary>The suspicious status (ACTIVE_MONITORING, RESTRICTED, or NO_TREATMENT).</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>The type(s) of suspicious user classification.</summary>
    [JsonPropertyName("types")]
    public string[]? Types { get; init; }
}
