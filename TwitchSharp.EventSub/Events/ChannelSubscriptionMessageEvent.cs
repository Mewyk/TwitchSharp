using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.subscription.message v1 EventSub event, fired when a user
/// sends a resubscription message in the specified broadcaster's channel.
/// </summary>
public sealed record ChannelSubscriptionMessageEvent
{
    /// <summary>The user ID of the subscriber.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login name of the subscriber.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the subscriber.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The tier of the subscription (1000, 2000, or 3000).</summary>
    [JsonPropertyName("tier")]
    public string Tier { get; init; } = string.Empty;

    /// <summary>The resubscription message and any emote data.</summary>
    [JsonPropertyName("message")]
    public SubscriptionMessageData Message { get; init; } = new();

    /// <summary>The total number of months the user has been subscribed.</summary>
    [JsonPropertyName("cumulative_months")]
    public int CumulativeMonths { get; init; }

    /// <summary>The number of consecutive months the user has been subscribed, or null if not shared.</summary>
    [JsonPropertyName("streak_months")]
    public int? StreakMonths { get; init; }

    /// <summary>The month duration of the subscription.</summary>
    [JsonPropertyName("duration_months")]
    public int DurationMonths { get; init; }
}
