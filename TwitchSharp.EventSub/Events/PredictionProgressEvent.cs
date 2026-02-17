using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a channel prediction progress event.</summary>
public sealed record PredictionProgressEvent
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

    /// <summary>The possible outcomes with current prediction data.</summary>
    [JsonPropertyName("outcomes")]
    public PredictionOutcomeData[] Outcomes { get; init; } = [];

    /// <summary>The timestamp when the prediction started.</summary>
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;

    /// <summary>The timestamp when the prediction will lock.</summary>
    [JsonPropertyName("locks_at")]
    public string LocksAt { get; init; } = string.Empty;
}
