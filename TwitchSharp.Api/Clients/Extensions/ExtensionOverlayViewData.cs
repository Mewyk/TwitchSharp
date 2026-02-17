using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the video overlay view configuration for an extension.
/// </summary>
public sealed record ExtensionOverlayViewData
{
    /// <summary>The HTML file shown to viewers in a video overlay slot.</summary>
    [JsonPropertyName("viewer_url")]
    public string ViewerUrl { get; init; } = string.Empty;

    /// <summary>Whether the extension can link to non-Twitch domains.</summary>
    [JsonPropertyName("can_link_external_content")]
    public bool CanLinkExternalContent { get; init; }
}
