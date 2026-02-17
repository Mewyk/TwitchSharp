using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents chat settings returned by the Get/Update Chat Settings endpoints.
/// </summary>
public sealed record ChatSettingsData
{
    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>Whether emote-only mode is enabled.</summary>
    [JsonPropertyName("emote_mode")]
    public bool EmoteMode { get; init; }

    /// <summary>Whether follower-only mode is enabled.</summary>
    [JsonPropertyName("follower_mode")]
    public bool FollowerMode { get; init; }

    /// <summary>The minimum follow duration in minutes required to chat. Null if follower mode is disabled.</summary>
    [JsonPropertyName("follower_mode_duration")]
    public int? FollowerModeDuration { get; init; }

    /// <summary>The moderator's user ID. Only present when the moderator_id parameter is used.</summary>
    [JsonPropertyName("moderator_id")]
    public string? ModeratorId { get; init; }

    /// <summary>Whether non-moderator chat delay is enabled. Only present with moderator scope.</summary>
    [JsonPropertyName("non_moderator_chat_delay")]
    public bool? NonModeratorChatDelay { get; init; }

    /// <summary>The non-moderator chat delay in seconds. Null if delay is disabled.</summary>
    [JsonPropertyName("non_moderator_chat_delay_duration")]
    public int? NonModeratorChatDelayDuration { get; init; }

    /// <summary>Whether slow mode is enabled.</summary>
    [JsonPropertyName("slow_mode")]
    public bool SlowMode { get; init; }

    /// <summary>The slow mode wait time in seconds. Null if slow mode is disabled.</summary>
    [JsonPropertyName("slow_mode_wait_time")]
    public int? SlowModeWaitTime { get; init; }

    /// <summary>Whether subscriber-only mode is enabled.</summary>
    [JsonPropertyName("subscriber_mode")]
    public bool SubscriberMode { get; init; }

    /// <summary>Whether unique chat mode (formerly R9K) is enabled.</summary>
    [JsonPropertyName("unique_chat_mode")]
    public bool UniqueChatMode { get; init; }
}
