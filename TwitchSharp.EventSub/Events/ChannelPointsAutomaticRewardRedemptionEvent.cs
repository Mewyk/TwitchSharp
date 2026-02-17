using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.channel_points_automatic_reward_redemption.add EventSub event,
/// fired when a viewer redeems an automatic channel points reward in the specified channel.
/// </summary>
public sealed record ChannelPointsAutomaticRewardRedemptionEvent
{
    /// <summary>The redemption identifier.</summary>
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

    /// <summary>The user ID of the viewer who redeemed the reward.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The login name of the viewer who redeemed the reward.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The display name of the viewer who redeemed the reward.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The automatic reward that was redeemed.</summary>
    [JsonPropertyName("reward")]
    public AutomaticRewardData Reward { get; init; } = new();

    /// <summary>The UTC timestamp of when the reward was redeemed.</summary>
    [JsonPropertyName("redeemed_at")]
    public string RedeemedAt { get; init; } = string.Empty;

    /// <summary>The message associated with the redemption, if any.</summary>
    [JsonPropertyName("message")]
    public AutomaticRewardMessageData? Message { get; init; }
}
