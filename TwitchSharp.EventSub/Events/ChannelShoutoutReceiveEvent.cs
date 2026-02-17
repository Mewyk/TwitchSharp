using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.shoutout.receive v1 EventSub event, fired when
/// the specified broadcaster receives a Shoutout from another channel.
/// </summary>
public sealed record ChannelShoutoutReceiveEvent
{
    /// <summary>The broadcaster's user ID (the one receiving the Shoutout).</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login name (the one receiving the Shoutout).</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name (the one receiving the Shoutout).</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The user ID of the broadcaster who sent the Shoutout.</summary>
    [JsonPropertyName("from_broadcaster_user_id")]
    public string FromBroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The user login name of the broadcaster who sent the Shoutout.</summary>
    [JsonPropertyName("from_broadcaster_user_login")]
    public string FromBroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the broadcaster who sent the Shoutout.</summary>
    [JsonPropertyName("from_broadcaster_user_name")]
    public string FromBroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The number of viewers in the sending broadcaster's stream at the time of the Shoutout.</summary>
    [JsonPropertyName("viewer_count")]
    public int ViewerCount { get; init; }

    /// <summary>The UTC timestamp of when the Shoutout was received.</summary>
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;
}
