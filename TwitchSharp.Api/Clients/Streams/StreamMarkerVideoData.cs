using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a video's stream markers, nested within <see cref="StreamMarkerContainerData"/>.
/// </summary>
public sealed record StreamMarkerVideoData
{
    /// <summary>The video's ID.</summary>
    [JsonPropertyName("video_id")]
    public string VideoId { get; init; } = string.Empty;

    /// <summary>The markers in this video.</summary>
    [JsonPropertyName("markers")]
    public StreamMarkerData[] Markers { get; init; } = [];
}
