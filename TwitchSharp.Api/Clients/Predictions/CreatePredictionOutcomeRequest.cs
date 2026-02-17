using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// An outcome in a Create Prediction request.
/// </summary>
public sealed record CreatePredictionOutcomeRequest
{
    /// <summary>The outcome text (max 25 characters). Required.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;
}
