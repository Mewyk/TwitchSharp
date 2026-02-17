using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a vacation period in a broadcaster's schedule.
/// </summary>
public sealed record ScheduleVacationData
{
    /// <summary>The UTC date and time (in RFC3339 format) when the vacation starts.</summary>
    [JsonPropertyName("start_time")]
    public string StartTime { get; init; } = string.Empty;

    /// <summary>The UTC date and time (in RFC3339 format) when the vacation ends.</summary>
    [JsonPropertyName("end_time")]
    public string EndTime { get; init; } = string.Empty;
}
