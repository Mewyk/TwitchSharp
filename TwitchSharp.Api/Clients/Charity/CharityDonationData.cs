using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a donation to a charity campaign.
/// </summary>
public sealed record CharityDonationData
{
    /// <summary>The donation ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The ID of the charity campaign.</summary>
    [JsonPropertyName("campaign_id")]
    public string CampaignId { get; init; } = string.Empty;

    /// <summary>The ID of the user who made the donation.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The donation amount.</summary>
    [JsonPropertyName("amount")]
    public CharityAmountData? Amount { get; init; }
}
