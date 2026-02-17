using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a date range in an analytics response.
/// </summary>
public sealed record AnalyticsDateRangeData
{
    /// <summary>The start of the date range (RFC3339).</summary>
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;

    /// <summary>The end of the date range (RFC3339).</summary>
    [JsonPropertyName("ended_at")]
    public string EndedAt { get; init; } = string.Empty;
}
