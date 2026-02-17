using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a badge version within a <see cref="BadgeSetData"/>.
/// </summary>
public sealed record BadgeVersionData
{
    /// <summary>The badge version's ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The URL for the 1x size badge image.</summary>
    [JsonPropertyName("image_url_1x")]
    public string ImageUrl1x { get; init; } = string.Empty;

    /// <summary>The URL for the 2x size badge image.</summary>
    [JsonPropertyName("image_url_2x")]
    public string ImageUrl2x { get; init; } = string.Empty;

    /// <summary>The URL for the 4x size badge image.</summary>
    [JsonPropertyName("image_url_4x")]
    public string ImageUrl4x { get; init; } = string.Empty;

    /// <summary>The badge's title.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The badge's description.</summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    /// <summary>The action to take when clicking the badge.</summary>
    [JsonPropertyName("click_action")]
    public string? ClickAction { get; init; }

    /// <summary>The URL to navigate to when clicking the badge.</summary>
    [JsonPropertyName("click_url")]
    public string? ClickUrl { get; init; }
}
