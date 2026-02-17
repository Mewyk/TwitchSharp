using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a top predictor for a prediction outcome.
/// </summary>
public sealed record TopPredictorData
{
    /// <summary>The predictor's user ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The predictor's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The predictor's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>Channel Points spent by this predictor.</summary>
    [JsonPropertyName("channel_points_used")]
    public int ChannelPointsUsed { get; init; }

    /// <summary>Channel Points won by this predictor.</summary>
    [JsonPropertyName("channel_points_won")]
    public int ChannelPointsWon { get; init; }
}
