using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a Guest Star invitation.
/// This API is in BETA and may change without notice.
/// </summary>
public sealed record GuestStarInviteData
{
    /// <summary>The invited user's ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>When the invitation was sent, in RFC3339 format.</summary>
    [JsonPropertyName("invited_at")]
    public string InvitedAt { get; init; } = string.Empty;

    /// <summary>The invitation status (INVITED, ACCEPTED, or READY).</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>Whether the guest's audio is enabled.</summary>
    [JsonPropertyName("is_audio_enabled")]
    public bool IsAudioEnabled { get; init; }

    /// <summary>Whether the guest's video is enabled.</summary>
    [JsonPropertyName("is_video_enabled")]
    public bool IsVideoEnabled { get; init; }

    /// <summary>Whether the guest's audio source is available.</summary>
    [JsonPropertyName("is_audio_available")]
    public bool IsAudioAvailable { get; init; }

    /// <summary>Whether the guest's video source is available.</summary>
    [JsonPropertyName("is_video_available")]
    public bool IsVideoAvailable { get; init; }
}
