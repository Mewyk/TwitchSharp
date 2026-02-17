using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the End Prediction endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record EndPredictionRequest
{
    /// <summary>The broadcaster's ID. Must match the user ID in the token. Required.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The prediction ID to update. Required.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The status to set: "RESOLVED", "CANCELED", or "LOCKED". Required.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>The ID of the winning outcome. Required if status is RESOLVED.</summary>
    [JsonPropertyName("winning_outcome_id")]
    public string? WinningOutcomeId { get; init; }
}
