using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.subscription.gift v1 EventSub event, fired when a user
/// gifts one or more subscriptions in the specified broadcaster's channel.
/// </summary>
public sealed record ChannelSubscriptionGiftEvent
{
    /// <summary>The user ID of the gifter.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login name of the gifter.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the gifter.</summary>
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

    /// <summary>The number of subscriptions gifted in this event.</summary>
    [JsonPropertyName("total")]
    public int Total { get; init; }

    /// <summary>The tier of the gifted subscriptions (1000, 2000, or 3000).</summary>
    [JsonPropertyName("tier")]
    public string Tier { get; init; } = string.Empty;

    /// <summary>The cumulative total of subscriptions gifted by this user, or null if anonymous.</summary>
    [JsonPropertyName("cumulative_total")]
    public int? CumulativeTotal { get; init; }

    /// <summary>Whether the gift was sent anonymously.</summary>
    [JsonPropertyName("is_anonymous")]
    public bool IsAnonymous { get; init; }
}
