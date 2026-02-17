using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a Twitch prediction.
/// </summary>
public sealed record PredictionData
{
    /// <summary>The prediction ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The broadcaster's ID.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_name")]
    public string BroadcasterName { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_login")]
    public string BroadcasterLogin { get; init; } = string.Empty;

    /// <summary>The prediction question.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The winning outcome ID, or null unless status is RESOLVED.</summary>
    [JsonPropertyName("winning_outcome_id")]
    public string? WinningOutcomeId { get; init; }

    /// <summary>The prediction outcomes.</summary>
    [JsonPropertyName("outcomes")]
    public PredictionOutcomeData[]? Outcomes { get; init; }

    /// <summary>The prediction duration in seconds.</summary>
    [JsonPropertyName("prediction_window")]
    public int PredictionWindow { get; init; }

    /// <summary>The prediction status: ACTIVE, CANCELED, LOCKED, or RESOLVED.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>The UTC date and time (in RFC3339 format) when the prediction was created.</summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = string.Empty;

    /// <summary>The UTC date and time (in RFC3339 format) when the prediction ended, or null if active.</summary>
    [JsonPropertyName("ended_at")]
    public string? EndedAt { get; init; }

    /// <summary>The UTC date and time (in RFC3339 format) when the prediction was locked, or null.</summary>
    [JsonPropertyName("locked_at")]
    public string? LockedAt { get; init; }
}
