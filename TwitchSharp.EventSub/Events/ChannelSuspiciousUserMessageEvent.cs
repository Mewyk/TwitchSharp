using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.suspicious_user.message v1 EventSub event, fired when
/// a suspicious user sends a message in the specified broadcaster's channel.
/// </summary>
public sealed record ChannelSuspiciousUserMessageEvent
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

    /// <summary>The user ID of the suspicious user.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login name of the suspicious user.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the suspicious user.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The low trust status of the user (e.g., "none", "active_monitoring", "restricted").</summary>
    [JsonPropertyName("low_trust_status")]
    public string LowTrustStatus { get; init; } = string.Empty;

    /// <summary>The channel IDs where the user is banned that contributed to the suspicious designation.</summary>
    [JsonPropertyName("shared_ban_channel_ids")]
    public string[] SharedBanChannelIds { get; init; } = [];

    /// <summary>The types of suspicious activity detected (e.g., "manually_added", "ban_evader_detector", "shared_channel_ban").</summary>
    [JsonPropertyName("types")]
    public string[] Types { get; init; } = [];

    /// <summary>The ban evasion evaluation result (e.g., "unknown", "possible", "likely").</summary>
    [JsonPropertyName("ban_evasion_evaluation")]
    public string BanEvasionEvaluation { get; init; } = string.Empty;

    /// <summary>The message sent by the suspicious user.</summary>
    [JsonPropertyName("message")]
    public SuspiciousUserMessageData Message { get; init; } = new();
}
