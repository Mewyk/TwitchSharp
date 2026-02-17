using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents channel Guest Star settings.
/// This API is in BETA and may change without notice.
/// </summary>
public sealed record GuestStarSettingsData
{
    /// <summary>Whether moderators can send guests live.</summary>
    [JsonPropertyName("is_moderator_send_live_enabled")]
    public bool IsModeratorSendLiveEnabled { get; init; }

    /// <summary>Number of Guest Star slots available (1-6).</summary>
    [JsonPropertyName("slot_count")]
    public int SlotCount { get; init; }

    /// <summary>Whether the browser source audio is enabled.</summary>
    [JsonPropertyName("is_browser_source_audio_enabled")]
    public bool IsBrowserSourceAudioEnabled { get; init; }

    /// <summary>The group layout type (e.g., TILED_LAYOUT, SCREENSHARE_LAYOUT, HORIZONTAL_LAYOUT, VERTICAL_LAYOUT).</summary>
    [JsonPropertyName("group_layout")]
    public string GroupLayout { get; init; } = string.Empty;

    /// <summary>The token for the OBS browser source.</summary>
    [JsonPropertyName("browser_source_token")]
    public string BrowserSourceToken { get; init; } = string.Empty;
}
