using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Update Channel Stream Schedule Segment endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record UpdateScheduleSegmentRequest
{
    /// <summary>The date and time when the broadcast starts (RFC3339 format).</summary>
    [JsonPropertyName("start_time")]
    public string? StartTime { get; init; }

    /// <summary>The broadcast duration in minutes (30-1380).</summary>
    [JsonPropertyName("duration")]
    public string? Duration { get; init; }

    /// <summary>The category ID for the broadcast.</summary>
    [JsonPropertyName("category_id")]
    public string? CategoryId { get; init; }

    /// <summary>The broadcast title (max 140 characters).</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>Set to true to cancel the segment.</summary>
    [JsonPropertyName("is_canceled")]
    public bool? IsCanceled { get; init; }

    /// <summary>The time zone using IANA format (e.g., "America/New_York").</summary>
    [JsonPropertyName("timezone")]
    public string? Timezone { get; init; }
}
