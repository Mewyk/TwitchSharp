using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents subscription gift data for a chat notification.</summary>
public sealed record ChatNotificationSubGiftData
{
    /// <summary>The duration of the gifted subscription in months.</summary>
    [JsonPropertyName("duration_months")]
    public int DurationMonths { get; init; }

    /// <summary>The cumulative total of gifts given by this user.</summary>
    [JsonPropertyName("cumulative_total")]
    public int? CumulativeTotal { get; init; }

    /// <summary>The recipient's user ID.</summary>
    [JsonPropertyName("recipient_user_id")]
    public string RecipientUserId { get; init; } = string.Empty;

    /// <summary>The recipient's user display name.</summary>
    [JsonPropertyName("recipient_user_name")]
    public string RecipientUserName { get; init; } = string.Empty;

    /// <summary>The recipient's user login.</summary>
    [JsonPropertyName("recipient_user_login")]
    public string RecipientUserLogin { get; init; } = string.Empty;

    /// <summary>The subscription tier.</summary>
    [JsonPropertyName("sub_tier")]
    public string SubTier { get; init; } = string.Empty;

    /// <summary>The community gift identifier if part of a community gift.</summary>
    [JsonPropertyName("community_gift_id")]
    public string? CommunityGiftId { get; init; }
}
