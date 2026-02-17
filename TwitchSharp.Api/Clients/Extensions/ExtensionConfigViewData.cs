using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the configuration view for an extension shown in the Extension Manager.
/// </summary>
public sealed record ExtensionConfigViewData
{
    /// <summary>The HTML file shown to broadcasters in the Extension Manager.</summary>
    [JsonPropertyName("viewer_url")]
    public string ViewerUrl { get; init; } = string.Empty;

    /// <summary>Whether the extension can link to non-Twitch domains.</summary>
    [JsonPropertyName("can_link_external_content")]
    public bool CanLinkExternalContent { get; init; }
}
