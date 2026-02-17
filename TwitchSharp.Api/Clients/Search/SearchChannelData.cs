using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a channel returned by the Search Channels endpoint.
/// </summary>
public sealed record SearchChannelData
{
    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_login")]
    public string BroadcasterLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; init; } = string.Empty;

    /// <summary>The ID of the game being played.</summary>
    [JsonPropertyName("game_id")]
    public string GameId { get; init; } = string.Empty;

    /// <summary>The name of the game being played.</summary>
    [JsonPropertyName("game_name")]
    public string GameName { get; init; } = string.Empty;

    /// <summary>The channel's stream title.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The broadcaster's language (ISO 639-1 two-letter code).</summary>
    [JsonPropertyName("broadcaster_language")]
    public string BroadcasterLanguage { get; init; } = string.Empty;

    /// <summary>The tags applied to the channel.</summary>
    [JsonPropertyName("tags")]
    public string[] Tags { get; init; } = [];

    /// <summary>The URL of the channel's thumbnail/profile image.</summary>
    [JsonPropertyName("thumbnail_url")]
    public string ThumbnailUrl { get; init; } = string.Empty;

    /// <summary>Whether the channel is currently live.</summary>
    [JsonPropertyName("is_live")]
    public bool IsLive { get; init; }

    /// <summary>The date and time the stream started. Empty string if not live.</summary>
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;
}
