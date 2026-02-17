using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Nested top predictor data for prediction outcome events.</summary>
public sealed record TopPredictorData
{
    /// <summary>The user identifier of the predictor.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The login name of the predictor.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The display name of the predictor.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The number of channel points won, or null if the prediction has not ended.</summary>
    [JsonPropertyName("channel_points_won")]
    public int? ChannelPointsWon { get; init; }

    /// <summary>The number of channel points used for the prediction.</summary>
    [JsonPropertyName("channel_points_used")]
    public int ChannelPointsUsed { get; init; }
}
