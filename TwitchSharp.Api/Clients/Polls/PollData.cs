using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a Twitch poll.
/// </summary>
public sealed record PollData
{
    /// <summary>The poll ID.</summary>
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

    /// <summary>The poll question.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The poll choices.</summary>
    [JsonPropertyName("choices")]
    public PollChoiceData[]? Choices { get; init; }

    /// <summary>Not used. Always false.</summary>
    [JsonPropertyName("bits_voting_enabled")]
    public bool BitsVotingEnabled { get; init; }

    /// <summary>Not used. Always 0.</summary>
    [JsonPropertyName("bits_per_vote")]
    public int BitsPerVote { get; init; }

    /// <summary>Whether viewers can cast votes using Channel Points.</summary>
    [JsonPropertyName("channel_points_voting_enabled")]
    public bool ChannelPointsVotingEnabled { get; init; }

    /// <summary>The Channel Points cost per additional vote.</summary>
    [JsonPropertyName("channel_points_per_vote")]
    public int ChannelPointsPerVote { get; init; }

    /// <summary>The poll status: ACTIVE, COMPLETED, TERMINATED, ARCHIVED, MODERATED, or INVALID.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>The poll runtime in seconds.</summary>
    [JsonPropertyName("duration")]
    public int Duration { get; init; }

    /// <summary>The UTC date and time (in RFC3339 format) when the poll started.</summary>
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;

    /// <summary>The UTC date and time (in RFC3339 format) when the poll ended, or null if active.</summary>
    [JsonPropertyName("ended_at")]
    public string? EndedAt { get; init; }
}
