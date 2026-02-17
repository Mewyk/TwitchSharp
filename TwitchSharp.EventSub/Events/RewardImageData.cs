using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents image URLs for a channel points custom reward.
/// </summary>
public sealed record RewardImageData
{
    /// <summary>The URL to a small version of the image.</summary>
    [JsonPropertyName("url_1x")]
    public string Url1X { get; init; } = string.Empty;

    /// <summary>The URL to a medium version of the image.</summary>
    [JsonPropertyName("url_2x")]
    public string Url2X { get; init; } = string.Empty;

    /// <summary>The URL to a large version of the image.</summary>
    [JsonPropertyName("url_4x")]
    public string Url4X { get; init; } = string.Empty;
}
