using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a channel points custom reward redemption event (add or update).</summary>
public sealed record ChannelPointsRedemptionEvent
{
    /// <summary>The redemption identifier.</summary>
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

    /// <summary>The user identifier of the redeemer.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The login name of the redeemer.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The display name of the redeemer.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The text input provided by the user during redemption.</summary>
    [JsonPropertyName("user_input")]
    public string UserInput { get; init; } = string.Empty;

    /// <summary>The redemption status.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>The reward data associated with this redemption.</summary>
    [JsonPropertyName("reward")]
    public RedemptionRewardData Reward { get; init; } = new();

    /// <summary>The timestamp when the reward was redeemed.</summary>
    [JsonPropertyName("redeemed_at")]
    public string RedeemedAt { get; init; } = string.Empty;
}
