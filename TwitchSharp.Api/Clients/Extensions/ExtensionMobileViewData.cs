using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the mobile view configuration for an extension.
/// </summary>
public sealed record ExtensionMobileViewData
{
    /// <summary>The HTML file shown to viewers on mobile devices.</summary>
    [JsonPropertyName("viewer_url")]
    public string ViewerUrl { get; init; } = string.Empty;
}
