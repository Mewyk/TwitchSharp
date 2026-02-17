using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the image URLs for an emote at different sizes.
/// </summary>
public sealed record EmoteImagesData
{
    /// <summary>The URL for the 1x size emote image.</summary>
    [JsonPropertyName("url_1x")]
    public string Url1x { get; init; } = string.Empty;

    /// <summary>The URL for the 2x size emote image.</summary>
    [JsonPropertyName("url_2x")]
    public string Url2x { get; init; } = string.Empty;

    /// <summary>The URL for the 4x size emote image.</summary>
    [JsonPropertyName("url_4x")]
    public string Url4x { get; init; } = string.Empty;
}
