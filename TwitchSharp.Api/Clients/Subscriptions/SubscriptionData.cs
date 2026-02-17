using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a broadcaster subscription returned by the Get Broadcaster Subscriptions endpoint.
/// </summary>
public sealed record SubscriptionData
{
    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_login")]
    public string BroadcasterLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_name")]
    public string BroadcasterName { get; init; } = string.Empty;

    /// <summary>The gifter's user ID. Empty if not a gift subscription.</summary>
    [JsonPropertyName("gifter_id")]
    public string GifterId { get; init; } = string.Empty;

    /// <summary>The gifter's login name. Empty if not a gift subscription.</summary>
    [JsonPropertyName("gifter_login")]
    public string GifterLogin { get; init; } = string.Empty;

    /// <summary>The gifter's display name. Empty if not a gift subscription.</summary>
    [JsonPropertyName("gifter_name")]
    public string GifterName { get; init; } = string.Empty;

    /// <summary>Whether this is a gift subscription.</summary>
    [JsonPropertyName("is_gift")]
    public bool IsGift { get; init; }

    /// <summary>The subscription plan name.</summary>
    [JsonPropertyName("plan_name")]
    public string PlanName { get; init; } = string.Empty;

    /// <summary>The subscription tier: "1000" (Tier 1), "2000" (Tier 2), or "3000" (Tier 3).</summary>
    [JsonPropertyName("tier")]
    public string Tier { get; init; } = string.Empty;

    /// <summary>The subscriber's user ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The subscriber's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The subscriber's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;
}
