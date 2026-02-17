using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.ad_break.begin v1 EventSub event, fired when an
/// ad break begins in the specified broadcaster's channel.
/// </summary>
public sealed record ChannelAdBreakBeginEvent
{
    /// <summary>The duration of the ad break in seconds.</summary>
    [JsonPropertyName("duration_seconds")]
    public int DurationSeconds { get; init; }

    /// <summary>The UTC timestamp of when the ad break began.</summary>
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;

    /// <summary>Whether the ad break was automatically triggered.</summary>
    [JsonPropertyName("is_automatic")]
    public bool IsAutomatic { get; init; }

    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The user ID of the user who requested the ad break.</summary>
    [JsonPropertyName("requester_user_id")]
    public string RequesterUserId { get; init; } = string.Empty;

    /// <summary>The user login name of the user who requested the ad break.</summary>
    [JsonPropertyName("requester_user_login")]
    public string RequesterUserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the user who requested the ad break.</summary>
    [JsonPropertyName("requester_user_name")]
    public string RequesterUserName { get; init; } = string.Empty;
}
