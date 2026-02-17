using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a custom reward redemption.
/// </summary>
public sealed record RedemptionData
{
    /// <summary>The ID that uniquely identifies the broadcaster.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_login")]
    public string BroadcasterLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_name")]
    public string BroadcasterName { get; init; } = string.Empty;

    /// <summary>The ID that uniquely identifies this redemption.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The ID of the user that redeemed the reward.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The text the user entered at the prompt, or empty if not required.</summary>
    [JsonPropertyName("user_input")]
    public string UserInput { get; init; } = string.Empty;

    /// <summary>The state of the redemption (CANCELED, FULFILLED, or UNFULFILLED).</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>The date and time of when the reward was redeemed (RFC3339 format).</summary>
    [JsonPropertyName("redeemed_at")]
    public string RedeemedAt { get; init; } = string.Empty;

    /// <summary>The reward that the user redeemed.</summary>
    [JsonPropertyName("reward")]
    public RedemptionRewardData? Reward { get; init; }
}
