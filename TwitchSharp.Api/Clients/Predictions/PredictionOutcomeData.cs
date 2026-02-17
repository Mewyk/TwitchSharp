using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a prediction outcome.
/// </summary>
public sealed record PredictionOutcomeData
{
    /// <summary>The outcome ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The outcome text.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The number of unique viewers who chose this outcome.</summary>
    [JsonPropertyName("users")]
    public int Users { get; init; }

    /// <summary>Total Channel Points spent on this outcome.</summary>
    [JsonPropertyName("channel_points")]
    public int ChannelPoints { get; init; }

    /// <summary>Top predictors for this outcome, or null if none.</summary>
    [JsonPropertyName("top_predictors")]
    public TopPredictorData[]? TopPredictors { get; init; }

    /// <summary>The outcome color: "BLUE" or "PINK".</summary>
    [JsonPropertyName("color")]
    public string Color { get; init; } = string.Empty;
}
