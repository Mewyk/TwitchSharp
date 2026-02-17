using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a channel poll progress event.</summary>
public sealed record PollProgressEvent
{
    /// <summary>The poll identifier.</summary>
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

    /// <summary>The poll question text.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The poll choices with current vote counts.</summary>
    [JsonPropertyName("choices")]
    public PollChoiceData[] Choices { get; init; } = [];

    /// <summary>The bits voting configuration for this poll.</summary>
    [JsonPropertyName("bits_voting")]
    public PollVotingData BitsVoting { get; init; } = new();

    /// <summary>The channel points voting configuration for this poll.</summary>
    [JsonPropertyName("channel_points_voting")]
    public PollVotingData ChannelPointsVoting { get; init; } = new();

    /// <summary>The timestamp when the poll started.</summary>
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;

    /// <summary>The timestamp when the poll will end.</summary>
    [JsonPropertyName("ends_at")]
    public string EndsAt { get; init; } = string.Empty;
}
