using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a Twitch video.
/// </summary>
public sealed record VideoData
{
    /// <summary>The video ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The stream ID if the video is an archive, otherwise null.</summary>
    [JsonPropertyName("stream_id")]
    public string? StreamId { get; init; }

    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The video title.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The video description.</summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    /// <summary>The UTC date and time (in RFC3339 format) when the video was created.</summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = string.Empty;

    /// <summary>The UTC date and time (in RFC3339 format) when the video was published.</summary>
    [JsonPropertyName("published_at")]
    public string PublishedAt { get; init; } = string.Empty;

    /// <summary>The video URL.</summary>
    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;

    /// <summary>The video thumbnail URL. Replace %{width} and %{height} with desired dimensions.</summary>
    [JsonPropertyName("thumbnail_url")]
    public string ThumbnailUrl { get; init; } = string.Empty;

    /// <summary>The video's viewability. Always "public".</summary>
    [JsonPropertyName("viewable")]
    public string Viewable { get; init; } = string.Empty;

    /// <summary>The total number of views.</summary>
    [JsonPropertyName("view_count")]
    public int ViewCount { get; init; }

    /// <summary>The ISO 639-1 language code of the video.</summary>
    [JsonPropertyName("language")]
    public string Language { get; init; } = string.Empty;

    /// <summary>The video type: "archive", "highlight", or "upload".</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The video duration in ISO 8601 format (e.g., "3m21s").</summary>
    [JsonPropertyName("duration")]
    public string Duration { get; init; } = string.Empty;

    /// <summary>The muted segments within the video, or null if none.</summary>
    [JsonPropertyName("muted_segments")]
    public MutedSegmentData[]? MutedSegments { get; init; }
}
