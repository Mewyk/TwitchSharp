using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents all view configurations for an extension.
/// </summary>
public sealed record ExtensionViewsData
{
    /// <summary>How the extension is displayed on mobile devices.</summary>
    [JsonPropertyName("mobile")]
    public ExtensionMobileViewData? Mobile { get; init; }

    /// <summary>How the extension is rendered as a panel extension.</summary>
    [JsonPropertyName("panel")]
    public ExtensionPanelViewData? Panel { get; init; }

    /// <summary>How the extension is rendered as a video overlay extension.</summary>
    [JsonPropertyName("video_overlay")]
    public ExtensionOverlayViewData? VideoOverlay { get; init; }

    /// <summary>How the extension is rendered as a video component extension.</summary>
    [JsonPropertyName("component")]
    public ExtensionComponentViewData? Component { get; init; }

    /// <summary>The view shown to broadcasters in the Extension Manager.</summary>
    [JsonPropertyName("config")]
    public ExtensionConfigViewData? Config { get; init; }
}
