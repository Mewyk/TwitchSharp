using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a creator goal.
/// </summary>
public sealed record GoalData
{
    /// <summary>An ID that identifies this goal.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>An ID that identifies the broadcaster that created the goal.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_name")]
    public string BroadcasterName { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_login")]
    public string BroadcasterLogin { get; init; } = string.Empty;

    /// <summary>The type of goal. Possible values: follower, subscription, subscription_count, new_subscription, new_subscription_count.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>A description of the goal. Is an empty string if not specified.</summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    /// <summary>The goal's current value.</summary>
    [JsonPropertyName("current_amount")]
    public int CurrentAmount { get; init; }

    /// <summary>The goal's target value.</summary>
    [JsonPropertyName("target_amount")]
    public int TargetAmount { get; init; }

    /// <summary>The UTC date and time (in RFC3339 format) that the broadcaster created the goal.</summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = string.Empty;
}
