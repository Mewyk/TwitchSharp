using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a Guest Star session end event. This event type is in beta.</summary>
public sealed record GuestStarSessionEndEvent
{
    /// <summary>The broadcaster's user identifier.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The unique session identifier.</summary>
    [JsonPropertyName("session_id")]
    public string SessionId { get; init; } = string.Empty;

    /// <summary>The timestamp when the session began.</summary>
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;

    /// <summary>The timestamp when the session ended.</summary>
    [JsonPropertyName("ended_at")]
    public string EndedAt { get; init; } = string.Empty;

    /// <summary>The host broadcaster's user identifier.</summary>
    [JsonPropertyName("host_user_id")]
    public string HostUserId { get; init; } = string.Empty;

    /// <summary>The host broadcaster's display name.</summary>
    [JsonPropertyName("host_user_name")]
    public string HostUserName { get; init; } = string.Empty;

    /// <summary>The host broadcaster's login name.</summary>
    [JsonPropertyName("host_user_login")]
    public string HostUserLogin { get; init; } = string.Empty;
}
