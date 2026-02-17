using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Create Poll endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record CreatePollRequest
{
    /// <summary>The broadcaster's ID. Must match the user ID in the token. Required.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The poll question (max 60 characters). Required.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The poll choices (min 2, max 5). Required.</summary>
    [JsonPropertyName("choices")]
    public CreatePollChoiceRequest[]? Choices { get; init; }

    /// <summary>The poll duration in seconds (15-1800). Required.</summary>
    [JsonPropertyName("duration")]
    public int Duration { get; init; }

    /// <summary>Whether to enable Channel Points voting.</summary>
    [JsonPropertyName("channel_points_voting_enabled")]
    public bool? ChannelPointsVotingEnabled { get; init; }

    /// <summary>Channel Points cost per additional vote (1-1000000).</summary>
    [JsonPropertyName("channel_points_per_vote")]
    public int? ChannelPointsPerVote { get; init; }
}
