using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.raid v1 EventSub event, fired when a broadcaster raids
/// another broadcaster's channel.
/// </summary>
public sealed record ChannelRaidEvent
{
    /// <summary>The user ID of the broadcaster sending the raid.</summary>
    [JsonPropertyName("from_broadcaster_user_id")]
    public string FromBroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The user login name of the broadcaster sending the raid.</summary>
    [JsonPropertyName("from_broadcaster_user_login")]
    public string FromBroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the broadcaster sending the raid.</summary>
    [JsonPropertyName("from_broadcaster_user_name")]
    public string FromBroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The user ID of the broadcaster receiving the raid.</summary>
    [JsonPropertyName("to_broadcaster_user_id")]
    public string ToBroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The user login name of the broadcaster receiving the raid.</summary>
    [JsonPropertyName("to_broadcaster_user_login")]
    public string ToBroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the broadcaster receiving the raid.</summary>
    [JsonPropertyName("to_broadcaster_user_name")]
    public string ToBroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The number of viewers in the raid.</summary>
    [JsonPropertyName("viewers")]
    public int Viewers { get; init; }
}
