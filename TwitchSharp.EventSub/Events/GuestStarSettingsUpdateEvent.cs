using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a Guest Star settings update event. This event type is in beta.</summary>
public sealed record GuestStarSettingsUpdateEvent
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

    /// <summary>Whether Guest Star moderators can control if a guest is live once assigned to a slot.</summary>
    [JsonPropertyName("is_moderator_send_live_enabled")]
    public bool IsModeratorSendLiveEnabled { get; init; }

    /// <summary>The number of slots the Guest Star call interface allows the host to add.</summary>
    [JsonPropertyName("slot_count")]
    public int SlotCount { get; init; }

    /// <summary>Whether browser sources subscribed to sessions on this channel should output audio.</summary>
    [JsonPropertyName("is_browser_source_audio_enabled")]
    public bool IsBrowserSourceAudioEnabled { get; init; }

    /// <summary>The layout for guests within a session in a group browser source (e.g., tiled, screenshare).</summary>
    [JsonPropertyName("group_layout")]
    public string GroupLayout { get; init; } = string.Empty;
}
