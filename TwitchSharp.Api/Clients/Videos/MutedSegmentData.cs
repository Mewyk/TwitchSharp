using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a muted segment within a video.
/// </summary>
public sealed record MutedSegmentData
{
    /// <summary>The duration of the muted segment in seconds.</summary>
    [JsonPropertyName("duration")]
    public int Duration { get; init; }

    /// <summary>The offset from the beginning of the video in seconds.</summary>
    [JsonPropertyName("offset")]
    public int Offset { get; init; }
}
