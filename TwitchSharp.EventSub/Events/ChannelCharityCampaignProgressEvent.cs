using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.charity_campaign.progress EventSub event, fired when
/// progress is made towards the campaign's goal or when the broadcaster
/// changes the fundraising goal.
/// </summary>
public sealed record ChannelCharityCampaignProgressEvent
{
    /// <summary>An ID that identifies the charity campaign.</summary>
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

    /// <summary>The current amount of donations that the campaign has received.</summary>
    [JsonPropertyName("current_amount")]
    public CharityCampaignAmountData CurrentAmount { get; init; } = new();

    /// <summary>The campaign's fundraising goal.</summary>
    [JsonPropertyName("target_amount")]
    public CharityCampaignAmountData TargetAmount { get; init; } = new();
}
