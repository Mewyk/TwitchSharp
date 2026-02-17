using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents audio or video media settings for a Guest Star guest.
/// This API is in BETA and may change without notice.
/// </summary>
public sealed record GuestStarMediaSettingsData
{
    /// <summary>Whether the media source is available from the guest.</summary>
    [JsonPropertyName("is_available")]
    public bool IsAvailable { get; init; }

    /// <summary>Whether the host has enabled this media source.</summary>
    [JsonPropertyName("is_host_enabled")]
    public bool IsHostEnabled { get; init; }

    /// <summary>Whether the guest has enabled this media source.</summary>
    [JsonPropertyName("is_guest_enabled")]
    public bool IsGuestEnabled { get; init; }
}
