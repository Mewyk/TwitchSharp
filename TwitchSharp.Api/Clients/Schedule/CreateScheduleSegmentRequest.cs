using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Create Channel Stream Schedule Segment endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record CreateScheduleSegmentRequest
{
    /// <summary>The date and time when the broadcast starts (RFC3339 format). Required.</summary>
    [JsonPropertyName("start_time")]
    public string StartTime { get; init; } = string.Empty;

    /// <summary>The time zone using IANA format (e.g., "America/New_York"). Required.</summary>
    [JsonPropertyName("timezone")]
    public string Timezone { get; init; } = string.Empty;

    /// <summary>The broadcast duration in minutes (30-1380). Required.</summary>
    [JsonPropertyName("duration")]
    public string Duration { get; init; } = string.Empty;

    /// <summary>Whether the broadcast recurs weekly.</summary>
    [JsonPropertyName("is_recurring")]
    public bool? IsRecurring { get; init; }

    /// <summary>The category ID for the broadcast.</summary>
    [JsonPropertyName("category_id")]
    public string? CategoryId { get; init; }

    /// <summary>The broadcast title (max 140 characters).</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }
}
