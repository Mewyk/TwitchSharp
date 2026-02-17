using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a stream marker returned by the Create Stream Marker and Get Stream Markers endpoints.
/// </summary>
public sealed record StreamMarkerData
{
    /// <summary>The marker's ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The date and time the marker was created.</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>The position in the stream where the marker was created, in seconds.</summary>
    [JsonPropertyName("position_seconds")]
    public int PositionSeconds { get; init; }

    /// <summary>The marker's description.</summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    /// <summary>The URL to the marker in the VOD. Only present in Get Stream Markers responses.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}
