using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a channel prediction end event.</summary>
public sealed record PredictionEndEvent
{
    /// <summary>The prediction identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The broadcaster's user identifier.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The prediction question text.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The identifier of the winning outcome.</summary>
    [JsonPropertyName("winning_outcome_id")]
    public string WinningOutcomeId { get; init; } = string.Empty;

    /// <summary>The possible outcomes with final prediction data.</summary>
    [JsonPropertyName("outcomes")]
    public PredictionOutcomeData[] Outcomes { get; init; } = [];

    /// <summary>The final status of the prediction.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>The timestamp when the prediction started.</summary>
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;

    /// <summary>The timestamp when the prediction ended.</summary>
    [JsonPropertyName("ended_at")]
    public string EndedAt { get; init; } = string.Empty;
}
