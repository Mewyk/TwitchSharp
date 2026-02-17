using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.chat_settings.update v1 EventSub event, fired when
/// the chat settings for the specified broadcaster's channel are updated.
/// </summary>
public sealed record ChannelChatSettingsUpdateEvent
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

    /// <summary>Whether emote-only mode is enabled.</summary>
    [JsonPropertyName("emote_mode")]
    public bool EmoteMode { get; init; }

    /// <summary>Whether follower-only mode is enabled.</summary>
    [JsonPropertyName("follower_mode")]
    public bool FollowerMode { get; init; }

    /// <summary>The number of minutes a user must follow before chatting, or null if follower mode is disabled.</summary>
    [JsonPropertyName("follower_mode_duration_minutes")]
    public int? FollowerModeDurationMinutes { get; init; }

    /// <summary>Whether slow mode is enabled.</summary>
    [JsonPropertyName("slow_mode")]
    public bool SlowMode { get; init; }

    /// <summary>The number of seconds between allowed messages in slow mode, or null if slow mode is disabled.</summary>
    [JsonPropertyName("slow_mode_wait_time_seconds")]
    public int? SlowModeWaitTimeSeconds { get; init; }

    /// <summary>Whether subscriber-only mode is enabled.</summary>
    [JsonPropertyName("subscriber_mode")]
    public bool SubscriberMode { get; init; }

    /// <summary>Whether unique chat mode (formerly R9K) is enabled.</summary>
    [JsonPropertyName("unique_chat_mode")]
    public bool UniqueChatMode { get; init; }
}
