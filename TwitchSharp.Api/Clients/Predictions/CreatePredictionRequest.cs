using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Create Prediction endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record CreatePredictionRequest
{
    /// <summary>The broadcaster's ID. Must match the user ID in the token. Required.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The prediction question (max 45 characters). Required.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The possible outcomes (min 2, max 10). Required.</summary>
    [JsonPropertyName("outcomes")]
    public CreatePredictionOutcomeRequest[]? Outcomes { get; init; }

    /// <summary>The prediction duration in seconds (30-1800). Required.</summary>
    [JsonPropertyName("prediction_window")]
    public int PredictionWindow { get; init; }
}
