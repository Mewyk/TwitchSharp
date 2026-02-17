using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a guest in a Guest Star session.
/// This API is in BETA and may change without notice.
/// </summary>
public sealed record GuestStarGuestData
{
    /// <summary>The slot identifier.</summary>
    [JsonPropertyName("slot_id")]
    public string SlotId { get; init; } = string.Empty;

    /// <summary>The guest's user ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The guest's display name.</summary>
    [JsonPropertyName("user_display_name")]
    public string UserDisplayName { get; init; } = string.Empty;

    /// <summary>The guest's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>Whether the guest is currently live.</summary>
    [JsonPropertyName("is_live")]
    public bool IsLive { get; init; }

    /// <summary>The guest's volume level (0-100).</summary>
    [JsonPropertyName("volume")]
    public int Volume { get; init; }

    /// <summary>When the guest was assigned to the slot, in RFC3339 format.</summary>
    [JsonPropertyName("assigned_at")]
    public string AssignedAt { get; init; } = string.Empty;

    /// <summary>The guest's audio settings.</summary>
    [JsonPropertyName("audio_settings")]
    public GuestStarMediaSettingsData AudioSettings { get; init; } = new();

    /// <summary>The guest's video settings.</summary>
    [JsonPropertyName("video_settings")]
    public GuestStarMediaSettingsData VideoSettings { get; init; } = new();
}
