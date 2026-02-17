using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a channel hype train end event (v2).</summary>
public sealed record HypeTrainEndEvent
{
    /// <summary>The hype train identifier.</summary>
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

    /// <summary>The final level reached by the hype train.</summary>
    [JsonPropertyName("level")]
    public int Level { get; init; }

    /// <summary>The total points contributed to the hype train.</summary>
    [JsonPropertyName("total")]
    public int Total { get; init; }

    /// <summary>The top contributions to the hype train.</summary>
    [JsonPropertyName("top_contributions")]
    public HypeTrainContributionData[] TopContributions { get; init; } = [];

    /// <summary>The timestamp when the hype train started.</summary>
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;

    /// <summary>The timestamp when the hype train ended.</summary>
    [JsonPropertyName("ended_at")]
    public string EndedAt { get; init; } = string.Empty;

    /// <summary>The timestamp when the cooldown period ends.</summary>
    [JsonPropertyName("cooldown_ends_at")]
    public string CooldownEndsAt { get; init; } = string.Empty;

    /// <summary>The type of hype train (e.g., treasure, golden_kappa, regular).</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>Whether this hype train is a shared hype train across multiple channels.</summary>
    [JsonPropertyName("is_shared_train")]
    public bool IsSharedTrain { get; init; }

    /// <summary>The participants in a shared hype train. Null if not a shared train.</summary>
    [JsonPropertyName("shared_train_participants")]
    public SharedTrainParticipantData[]? SharedTrainParticipants { get; init; }
}
