using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a Guest Star guest update event. This event type is in beta.</summary>
public sealed record GuestStarGuestUpdateEvent
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

    /// <summary>The moderator's user identifier. Null if the update was performed by the guest.</summary>
    [JsonPropertyName("moderator_user_id")]
    public string? ModeratorUserId { get; init; }

    /// <summary>The moderator's display name. Null if the update was performed by the guest.</summary>
    [JsonPropertyName("moderator_user_name")]
    public string? ModeratorUserName { get; init; }

    /// <summary>The moderator's login name. Null if the update was performed by the guest.</summary>
    [JsonPropertyName("moderator_user_login")]
    public string? ModeratorUserLogin { get; init; }

    /// <summary>The guest's user identifier. Null if the slot is now empty.</summary>
    [JsonPropertyName("guest_user_id")]
    public string? GuestUserId { get; init; }

    /// <summary>The guest's display name. Null if the slot is now empty.</summary>
    [JsonPropertyName("guest_user_name")]
    public string? GuestUserName { get; init; }

    /// <summary>The guest's login name. Null if the slot is now empty.</summary>
    [JsonPropertyName("guest_user_login")]
    public string? GuestUserLogin { get; init; }

    /// <summary>The slot assignment identifier. Null if the guest is in the invited, removed, ready, or accepted state.</summary>
    [JsonPropertyName("slot_id")]
    public string? SlotId { get; init; }

    /// <summary>The current state of the guest (e.g., invited, accepted, ready, backstage, live, removed). Null if the slot is now empty.</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>The host broadcaster's user identifier.</summary>
    [JsonPropertyName("host_user_id")]
    public string HostUserId { get; init; } = string.Empty;

    /// <summary>The host broadcaster's display name.</summary>
    [JsonPropertyName("host_user_name")]
    public string HostUserName { get; init; } = string.Empty;

    /// <summary>The host broadcaster's login name.</summary>
    [JsonPropertyName("host_user_login")]
    public string HostUserLogin { get; init; } = string.Empty;

    /// <summary>Whether the host is allowing the slot's video to be seen. Null if the guest is not slotted.</summary>
    [JsonPropertyName("host_video_enabled")]
    public bool? HostVideoEnabled { get; init; }

    /// <summary>Whether the host is allowing the slot's audio to be heard. Null if the guest is not slotted.</summary>
    [JsonPropertyName("host_audio_enabled")]
    public bool? HostAudioEnabled { get; init; }

    /// <summary>The slot's audio level (0-100) as heard by participants. Null if the guest is not slotted.</summary>
    [JsonPropertyName("host_volume")]
    public int? HostVolume { get; init; }
}
