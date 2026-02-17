using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.charity_campaign.donate EventSub event, fired when
/// a user donates to the broadcaster's active charity campaign.
/// </summary>
public sealed record ChannelCharityCampaignDonateEvent
{
    /// <summary>An ID that identifies the donation.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>An ID that identifies the charity campaign the donation applies to.</summary>
    [JsonPropertyName("campaign_id")]
    public string CampaignId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The user ID of the donor.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The login name of the donor.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The display name of the donor.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The charity's name.</summary>
    [JsonPropertyName("charity_name")]
    public string CharityName { get; init; } = string.Empty;

    /// <summary>A description of the charity.</summary>
    [JsonPropertyName("charity_description")]
    public string CharityDescription { get; init; } = string.Empty;

    /// <summary>A URL to an image of the charity's logo.</summary>
    [JsonPropertyName("charity_logo")]
    public string CharityLogo { get; init; } = string.Empty;

    /// <summary>A URL to the charity's website.</summary>
    [JsonPropertyName("charity_website")]
    public string CharityWebsite { get; init; } = string.Empty;

    /// <summary>The donation amount.</summary>
    [JsonPropertyName("amount")]
    public CharityCampaignAmountData Amount { get; init; } = new();
}
