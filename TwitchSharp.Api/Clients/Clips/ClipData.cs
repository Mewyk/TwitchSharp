using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a Twitch clip.
/// </summary>
public sealed record ClipData
{
    /// <summary>The unique clip ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The clip URL.</summary>
    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;

    /// <summary>The embeddable iframe URL.</summary>
    [JsonPropertyName("embed_url")]
    public string EmbedUrl { get; init; } = string.Empty;

    /// <summary>The broadcaster's ID.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_name")]
    public string BroadcasterName { get; init; } = string.Empty;

    /// <summary>The clip creator's ID.</summary>
    [JsonPropertyName("creator_id")]
    public string CreatorId { get; init; } = string.Empty;

    /// <summary>The clip creator's display name.</summary>
    [JsonPropertyName("creator_name")]
    public string CreatorName { get; init; } = string.Empty;

    /// <summary>The source video ID, or empty string if unavailable.</summary>
    [JsonPropertyName("video_id")]
    public string VideoId { get; init; } = string.Empty;

    /// <summary>The game ID at the time the clip was created.</summary>
    [JsonPropertyName("game_id")]
    public string GameId { get; init; } = string.Empty;

    /// <summary>The ISO 639-1 language code or "other".</summary>
    [JsonPropertyName("language")]
    public string Language { get; init; } = string.Empty;

    /// <summary>The clip title.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The total number of views.</summary>
    [JsonPropertyName("view_count")]
    public int ViewCount { get; init; }

    /// <summary>The UTC date and time (in RFC3339 format) when the clip was created.</summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = string.Empty;

    /// <summary>The clip thumbnail URL.</summary>
    [JsonPropertyName("thumbnail_url")]
    public string ThumbnailUrl { get; init; } = string.Empty;

    /// <summary>The clip length in seconds (precision 0.1).</summary>
    [JsonPropertyName("duration")]
    public double Duration { get; init; }

    /// <summary>The offset in the VOD where the clip starts in seconds, or null if unavailable.</summary>
    [JsonPropertyName("vod_offset")]
    public int? VodOffset { get; init; }

    /// <summary>Whether the clip is featured.</summary>
    [JsonPropertyName("is_featured")]
    public bool IsFeatured { get; init; }
}
