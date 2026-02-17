using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a shared chat session end event.</summary>
public sealed record SharedChatEndEvent
{
    /// <summary>The shared chat session identifier.</summary>
    [JsonPropertyName("session_id")]
    public string SessionId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user identifier.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The host broadcaster's user identifier.</summary>
    [JsonPropertyName("host_broadcaster_user_id")]
    public string HostBroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The host broadcaster's login name.</summary>
    [JsonPropertyName("host_broadcaster_user_login")]
    public string HostBroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The host broadcaster's display name.</summary>
    [JsonPropertyName("host_broadcaster_user_name")]
    public string HostBroadcasterUserName { get; init; } = string.Empty;
}
