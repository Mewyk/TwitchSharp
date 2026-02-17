using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.suspicious_user.update v1 EventSub event, fired when
/// a moderator updates the suspicious status of a user in the specified
/// broadcaster's channel.
/// </summary>
public sealed record ChannelSuspiciousUserUpdateEvent
{
    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The moderator's user ID who updated the suspicious status.</summary>
    [JsonPropertyName("moderator_user_id")]
    public string ModeratorUserId { get; init; } = string.Empty;

    /// <summary>The moderator's user login name who updated the suspicious status.</summary>
    [JsonPropertyName("moderator_user_login")]
    public string ModeratorUserLogin { get; init; } = string.Empty;

    /// <summary>The moderator's user display name who updated the suspicious status.</summary>
    [JsonPropertyName("moderator_user_name")]
    public string ModeratorUserName { get; init; } = string.Empty;

    /// <summary>The user ID of the suspicious user.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login name of the suspicious user.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the suspicious user.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The updated low trust status (e.g., "none", "active_monitoring", "restricted").</summary>
    [JsonPropertyName("low_trust_status")]
    public string LowTrustStatus { get; init; } = string.Empty;
}
