using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a set of images for a custom reward.
/// </summary>
public sealed record CustomRewardImageData
{
    /// <summary>The URL to a small version of the image.</summary>
    [JsonPropertyName("url_1x")]
    public string Url1x { get; init; } = string.Empty;

    /// <summary>The URL to a medium version of the image.</summary>
    [JsonPropertyName("url_2x")]
    public string Url2x { get; init; } = string.Empty;

    /// <summary>The URL to a large version of the image.</summary>
    [JsonPropertyName("url_4x")]
    public string Url4x { get; init; } = string.Empty;
}
