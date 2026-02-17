using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a charity campaign.
/// </summary>
public sealed record CharityCampaignData
{
    /// <summary>The charity campaign ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The broadcaster's ID.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_login")]
    public string BroadcasterLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_name")]
    public string BroadcasterName { get; init; } = string.Empty;

    /// <summary>The charity's name.</summary>
    [JsonPropertyName("charity_name")]
    public string CharityName { get; init; } = string.Empty;

    /// <summary>A description of the charity.</summary>
    [JsonPropertyName("charity_description")]
    public string CharityDescription { get; init; } = string.Empty;

    /// <summary>The URL to the charity's logo.</summary>
    [JsonPropertyName("charity_logo")]
    public string CharityLogo { get; init; } = string.Empty;

    /// <summary>The URL to the charity's website.</summary>
    [JsonPropertyName("charity_website")]
    public string CharityWebsite { get; init; } = string.Empty;

    /// <summary>The current amount raised.</summary>
    [JsonPropertyName("current_amount")]
    public CharityAmountData? CurrentAmount { get; init; }

    /// <summary>The target fundraising amount, or null if no target is set.</summary>
    [JsonPropertyName("target_amount")]
    public CharityAmountData? TargetAmount { get; init; }
}
