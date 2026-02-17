using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.subscription.end v1 EventSub event, fired when a user's
/// subscription to the specified broadcaster's channel ends.
/// </summary>
public sealed record ChannelSubscriptionEndEvent
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

    /// <summary>Whether the subscription was a gift.</summary>
    [JsonPropertyName("is_gift")]
    public bool IsGift { get; init; }
}
