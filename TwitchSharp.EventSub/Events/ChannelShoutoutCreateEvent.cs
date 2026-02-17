using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.shoutout.create v1 EventSub event, fired when
/// a Shoutout is sent from the specified broadcaster's channel.
/// </summary>
public sealed record ChannelShoutoutCreateEvent
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

    /// <summary>The moderator's user ID who sent the Shoutout.</summary>
    [JsonPropertyName("moderator_user_id")]
    public string ModeratorUserId { get; init; } = string.Empty;

    /// <summary>The moderator's user login name who sent the Shoutout.</summary>
    [JsonPropertyName("moderator_user_login")]
    public string ModeratorUserLogin { get; init; } = string.Empty;

    /// <summary>The moderator's user display name who sent the Shoutout.</summary>
    [JsonPropertyName("moderator_user_name")]
    public string ModeratorUserName { get; init; } = string.Empty;

    /// <summary>The user ID of the broadcaster receiving the Shoutout.</summary>
    [JsonPropertyName("to_broadcaster_user_id")]
    public string ToBroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The user login name of the broadcaster receiving the Shoutout.</summary>
    [JsonPropertyName("to_broadcaster_user_login")]
    public string ToBroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the broadcaster receiving the Shoutout.</summary>
    [JsonPropertyName("to_broadcaster_user_name")]
    public string ToBroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The number of viewers in the sending broadcaster's stream at the time of the Shoutout.</summary>
    [JsonPropertyName("viewer_count")]
    public int ViewerCount { get; init; }

    /// <summary>The UTC timestamp of when the Shoutout was sent.</summary>
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;

    /// <summary>The UTC timestamp of when the broadcaster may send another Shoutout without exceeding the rate limit.</summary>
    [JsonPropertyName("cooldown_ends_at")]
    public string CooldownEndsAt { get; init; } = string.Empty;

    /// <summary>The UTC timestamp of when the broadcaster may send another Shoutout to the same target without exceeding the rate limit.</summary>
    [JsonPropertyName("target_cooldown_ends_at")]
    public string TargetCooldownEndsAt { get; init; } = string.Empty;
}
