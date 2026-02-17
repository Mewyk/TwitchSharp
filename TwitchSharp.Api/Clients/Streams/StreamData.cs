using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a live stream returned by the Get Streams and Get Followed Streams endpoints.
/// </summary>
public sealed record StreamData
{
    /// <summary>The stream's ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The ID of the game being played.</summary>
    [JsonPropertyName("game_id")]
    public string GameId { get; init; } = string.Empty;

    /// <summary>The name of the game being played.</summary>
    [JsonPropertyName("game_name")]
    public string GameName { get; init; } = string.Empty;

    /// <summary>The stream type ("live" or "").</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The stream title.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The current number of viewers.</summary>
    [JsonPropertyName("viewer_count")]
    public int ViewerCount { get; init; }

    /// <summary>The date and time the stream started.</summary>
    [JsonPropertyName("started_at")]
    public DateTimeOffset StartedAt { get; init; }

    /// <summary>The stream language (ISO 639-1 two-letter code).</summary>
    [JsonPropertyName("language")]
    public string Language { get; init; } = string.Empty;

    /// <summary>The thumbnail URL template. Contains {width} and {height} placeholders.</summary>
    [JsonPropertyName("thumbnail_url")]
    public string ThumbnailUrl { get; init; } = string.Empty;

    /// <summary>The tags applied to the stream.</summary>
    [JsonPropertyName("tags")]
    public string[] Tags { get; init; } = [];

    /// <summary>Whether the stream is intended for mature audiences.</summary>
    [JsonPropertyName("is_mature")]
    public bool IsMature { get; init; }
}
