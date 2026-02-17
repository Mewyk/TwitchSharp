using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a scheduled broadcast segment.
/// </summary>
public sealed record ScheduleSegmentData
{
    /// <summary>The broadcast segment ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The UTC date and time (in RFC3339 format) when the broadcast starts.</summary>
    [JsonPropertyName("start_time")]
    public string StartTime { get; init; } = string.Empty;

    /// <summary>The UTC date and time (in RFC3339 format) when the broadcast ends.</summary>
    [JsonPropertyName("end_time")]
    public string EndTime { get; init; } = string.Empty;

    /// <summary>The broadcast title.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The cancellation date in ISO 8601 format, or null if not canceled.</summary>
    [JsonPropertyName("canceled_until")]
    public string? CanceledUntil { get; init; }

    /// <summary>The broadcast category, or null if not set.</summary>
    [JsonPropertyName("category")]
    public ScheduleCategoryData? Category { get; init; }

    /// <summary>Whether the broadcast recurs weekly.</summary>
    [JsonPropertyName("is_recurring")]
    public bool IsRecurring { get; init; }
}
