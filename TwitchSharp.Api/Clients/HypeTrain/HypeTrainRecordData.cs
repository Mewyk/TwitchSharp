using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a Hype Train record (all-time high or shared all-time high).
/// </summary>
public sealed record HypeTrainRecordData
{
    /// <summary>The level of the record Hype Train.</summary>
    [JsonPropertyName("level")]
    public int Level { get; init; }

    /// <summary>Total points contributed to the record Hype Train.</summary>
    [JsonPropertyName("total")]
    public int Total { get; init; }

    /// <summary>The time when the record was achieved (ISO 8601 format).</summary>
    [JsonPropertyName("achieved_at")]
    public string AchievedAt { get; init; } = string.Empty;
}
