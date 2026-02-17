using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents clip download information.
/// </summary>
public sealed record ClipDownloadData
{
    /// <summary>The unique clip ID.</summary>
    [JsonPropertyName("clip_id")]
    public string ClipId { get; init; } = string.Empty;

    /// <summary>The landscape format download URL, or null if unavailable.</summary>
    [JsonPropertyName("landscape_download_url")]
    public string? LandscapeDownloadUrl { get; init; }

    /// <summary>The portrait format download URL, or null if unavailable.</summary>
    [JsonPropertyName("portrait_download_url")]
    public string? PortraitDownloadUrl { get; init; }
}
