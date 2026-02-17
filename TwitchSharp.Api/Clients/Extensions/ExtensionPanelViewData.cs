using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the panel view configuration for an extension.
/// </summary>
public sealed record ExtensionPanelViewData
{
    /// <summary>The HTML file shown to viewers in a panel slot.</summary>
    [JsonPropertyName("viewer_url")]
    public string ViewerUrl { get; init; } = string.Empty;

    /// <summary>The height in pixels of the panel component.</summary>
    [JsonPropertyName("height")]
    public int Height { get; init; }

    /// <summary>Whether the extension can link to non-Twitch domains.</summary>
    [JsonPropertyName("can_link_external_content")]
    public bool CanLinkExternalContent { get; init; }
}
