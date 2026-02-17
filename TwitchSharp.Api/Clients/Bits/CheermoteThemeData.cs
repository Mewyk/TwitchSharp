using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the animated and static image sets for a Cheermote theme.
/// Each dictionary maps size keys ("1", "1.5", "2", "3", "4") to image URLs.
/// </summary>
public sealed record CheermoteThemeData
{
    /// <summary>Animated images (GIF format), keyed by size ("1", "1.5", "2", "3", "4").</summary>
    [JsonPropertyName("animated")]
    public Dictionary<string, string>? Animated { get; init; }

    /// <summary>Static images (PNG format), keyed by size ("1", "1.5", "2", "3", "4").</summary>
    [JsonPropertyName("static")]
    public Dictionary<string, string>? Static { get; init; }
}
