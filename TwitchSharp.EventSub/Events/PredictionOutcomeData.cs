using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Nested prediction outcome data for prediction events.</summary>
public sealed record PredictionOutcomeData
{
    /// <summary>The outcome identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The outcome title text.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The outcome color.</summary>
    [JsonPropertyName("color")]
    public string Color { get; init; } = string.Empty;

    /// <summary>The number of users who predicted this outcome.</summary>
    [JsonPropertyName("users")]
    public int Users { get; init; }

    /// <summary>The total channel points wagered on this outcome.</summary>
    [JsonPropertyName("channel_points")]
    public int ChannelPoints { get; init; }

    /// <summary>The top predictors for this outcome, if available.</summary>
    [JsonPropertyName("top_predictors")]
    public TopPredictorData[]? TopPredictors { get; init; }
}
