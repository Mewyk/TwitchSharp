using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a user's subscription status returned by the Check User Subscription endpoint.
/// </summary>
public sealed record UserSubscriptionData
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

    /// <summary>The gifter's user ID. Only present if the subscription is a gift.</summary>
    [JsonPropertyName("gifter_id")]
    public string? GifterId { get; init; }

    /// <summary>The gifter's login name. Only present if the subscription is a gift.</summary>
    [JsonPropertyName("gifter_login")]
    public string? GifterLogin { get; init; }

    /// <summary>The gifter's display name. Only present if the subscription is a gift.</summary>
    [JsonPropertyName("gifter_name")]
    public string? GifterName { get; init; }

    /// <summary>Whether this is a gift subscription.</summary>
    [JsonPropertyName("is_gift")]
    public bool IsGift { get; init; }

    /// <summary>The subscription tier: "1000" (Tier 1), "2000" (Tier 2), or "3000" (Tier 3).</summary>
    [JsonPropertyName("tier")]
    public string Tier { get; init; } = string.Empty;
}
