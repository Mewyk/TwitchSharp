using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents game analytics data.
/// </summary>
public sealed record GameAnalyticsData
{
    /// <summary>The game's ID.</summary>
    [JsonPropertyName("game_id")]
    public string GameId { get; init; } = string.Empty;

    /// <summary>The URL to the downloadable CSV file with analytics data.</summary>
    [JsonPropertyName("URL")]
    public string Url { get; init; } = string.Empty;

    /// <summary>The type of analytics report.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The date range for the report.</summary>
    [JsonPropertyName("date_range")]
    public AnalyticsDateRangeData? DateRange { get; init; }
}
