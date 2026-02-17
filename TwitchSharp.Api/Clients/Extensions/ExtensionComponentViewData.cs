using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the video component view configuration for an extension.
/// </summary>
public sealed record ExtensionComponentViewData
{
    /// <summary>The HTML file shown to viewers in a video component slot.</summary>
    [JsonPropertyName("viewer_url")]
    public string ViewerUrl { get; init; } = string.Empty;

    /// <summary>The width value of the aspect ratio (width : height).</summary>
    [JsonPropertyName("aspect_ratio_x")]
    public int AspectRatioX { get; init; }

    /// <summary>The height value of the aspect ratio (width : height).</summary>
    [JsonPropertyName("aspect_ratio_y")]
    public int AspectRatioY { get; init; }

    /// <summary>Whether CSS zoom is applied for fixed inner dimensions.</summary>
    [JsonPropertyName("autoscale")]
    public bool Autoscale { get; init; }

    /// <summary>The base width in pixels used when scaling (ignored if autoscale is false).</summary>
    [JsonPropertyName("scale_pixels")]
    public int ScalePixels { get; init; }

    /// <summary>The height as a percent (1-100) of the maximum height of a video component extension.</summary>
    [JsonPropertyName("target_height")]
    public int TargetHeight { get; init; }

    /// <summary>Whether the extension can link to non-Twitch domains.</summary>
    [JsonPropertyName("can_link_external_content")]
    public bool CanLinkExternalContent { get; init; }
}
