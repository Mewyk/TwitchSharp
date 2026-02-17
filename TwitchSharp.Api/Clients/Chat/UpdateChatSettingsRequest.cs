using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Update Chat Settings endpoint. All fields are optional.
/// </summary>
[GenerateWithMethods]
public sealed partial record UpdateChatSettingsRequest
{
    /// <summary>Whether to enable emote-only mode.</summary>
    [JsonPropertyName("emote_mode")]
    public bool? EmoteMode { get; init; }

    /// <summary>Whether to enable follower-only mode.</summary>
    [JsonPropertyName("follower_mode")]
    public bool? FollowerMode { get; init; }

    /// <summary>The minimum follow duration in minutes required to chat.</summary>
    [JsonPropertyName("follower_mode_duration")]
    public int? FollowerModeDuration { get; init; }

    /// <summary>Whether to enable non-moderator chat delay.</summary>
    [JsonPropertyName("non_moderator_chat_delay")]
    public bool? NonModeratorChatDelay { get; init; }

    /// <summary>The non-moderator chat delay in seconds.</summary>
    [JsonPropertyName("non_moderator_chat_delay_duration")]
    public int? NonModeratorChatDelayDuration { get; init; }

    /// <summary>Whether to enable slow mode.</summary>
    [JsonPropertyName("slow_mode")]
    public bool? SlowMode { get; init; }

    /// <summary>The slow mode wait time in seconds.</summary>
    [JsonPropertyName("slow_mode_wait_time")]
    public int? SlowModeWaitTime { get; init; }

    /// <summary>Whether to enable subscriber-only mode.</summary>
    [JsonPropertyName("subscriber_mode")]
    public bool? SubscriberMode { get; init; }

    /// <summary>Whether to enable unique chat mode.</summary>
    [JsonPropertyName("unique_chat_mode")]
    public bool? UniqueChatMode { get; init; }
}
