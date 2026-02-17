using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.goal.end v1 EventSub event, fired when a goal
/// ends in the specified broadcaster's channel.
/// </summary>
public sealed record ChannelGoalEndEvent
{
    /// <summary>An ID that identifies this goal.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The type of goal (e.g., follower, subscription).</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>A description of the goal, or empty if not specified.</summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    /// <summary>The goal's current value.</summary>
    [JsonPropertyName("current_amount")]
    public int CurrentAmount { get; init; }

    /// <summary>The goal's target value.</summary>
    [JsonPropertyName("target_amount")]
    public int TargetAmount { get; init; }

    /// <summary>The UTC timestamp of when the goal was created.</summary>
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;

    /// <summary>Whether the goal was achieved.</summary>
    [JsonPropertyName("is_achieved")]
    public bool IsAchieved { get; init; }

    /// <summary>The UTC timestamp of when the goal ended.</summary>
    [JsonPropertyName("ended_at")]
    public string EndedAt { get; init; } = string.Empty;
}
