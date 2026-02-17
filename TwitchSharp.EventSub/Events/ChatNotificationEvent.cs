using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a channel.chat.notification v1 event.</summary>
public sealed record ChatNotificationEvent
{
    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The chatter's user ID.</summary>
    [JsonPropertyName("chatter_user_id")]
    public string ChatterUserId { get; init; } = string.Empty;

    /// <summary>The chatter's user login.</summary>
    [JsonPropertyName("chatter_user_login")]
    public string ChatterUserLogin { get; init; } = string.Empty;

    /// <summary>The chatter's user display name.</summary>
    [JsonPropertyName("chatter_user_name")]
    public string ChatterUserName { get; init; } = string.Empty;

    /// <summary>Whether the chatter is anonymous.</summary>
    [JsonPropertyName("chatter_is_anonymous")]
    public bool ChatterIsAnonymous { get; init; }

    /// <summary>The color of the chatter's name.</summary>
    [JsonPropertyName("color")]
    public string Color { get; init; } = string.Empty;

    /// <summary>The badges associated with the chatter.</summary>
    [JsonPropertyName("badges")]
    public ChatBadgeData[] Badges { get; init; } = [];

    /// <summary>The system-generated notification message.</summary>
    [JsonPropertyName("system_message")]
    public string SystemMessage { get; init; } = string.Empty;

    /// <summary>The unique message identifier.</summary>
    [JsonPropertyName("message_id")]
    public string MessageId { get; init; } = string.Empty;

    /// <summary>The structured message content.</summary>
    [JsonPropertyName("message")]
    public ChatMessageData Message { get; init; } = new();

    /// <summary>The type of notification.</summary>
    [JsonPropertyName("notice_type")]
    public string NoticeType { get; init; } = string.Empty;

    /// <summary>The subscription data if the notice type is sub.</summary>
    [JsonPropertyName("sub")]
    public ChatNotificationSubData? Sub { get; init; }

    /// <summary>The resubscription data if the notice type is resub.</summary>
    [JsonPropertyName("resub")]
    public ChatNotificationResubData? Resub { get; init; }

    /// <summary>The subscription gift data if the notice type is sub_gift.</summary>
    [JsonPropertyName("sub_gift")]
    public ChatNotificationSubGiftData? SubGift { get; init; }

    /// <summary>The community subscription gift data if the notice type is community_sub_gift.</summary>
    [JsonPropertyName("community_sub_gift")]
    public ChatNotificationCommunitySubGiftData? CommunitySubGift { get; init; }

    /// <summary>The gift paid upgrade data if the notice type is gift_paid_upgrade.</summary>
    [JsonPropertyName("gift_paid_upgrade")]
    public ChatNotificationGiftPaidUpgradeData? GiftPaidUpgrade { get; init; }

    /// <summary>The prime paid upgrade data if the notice type is prime_paid_upgrade.</summary>
    [JsonPropertyName("prime_paid_upgrade")]
    public ChatNotificationPrimePaidUpgradeData? PrimePaidUpgrade { get; init; }

    /// <summary>The raid data if the notice type is raid.</summary>
    [JsonPropertyName("raid")]
    public ChatNotificationRaidData? Raid { get; init; }

    /// <summary>The unraid data if the notice type is unraid.</summary>
    [JsonPropertyName("unraid")]
    public ChatNotificationUnraidData? Unraid { get; init; }

    /// <summary>The pay it forward data if the notice type is pay_it_forward.</summary>
    [JsonPropertyName("pay_it_forward")]
    public ChatNotificationPayItForwardData? PayItForward { get; init; }

    /// <summary>The announcement data if the notice type is announcement.</summary>
    [JsonPropertyName("announcement")]
    public ChatNotificationAnnouncementData? Announcement { get; init; }

    /// <summary>The bits badge tier data if the notice type is bits_badge_tier.</summary>
    [JsonPropertyName("bits_badge_tier")]
    public ChatNotificationBitsBadgeTierData? BitsBadgeTier { get; init; }

    /// <summary>The charity donation data if the notice type is charity_donation.</summary>
    [JsonPropertyName("charity_donation")]
    public ChatNotificationCharityDonationData? CharityDonation { get; init; }

    /// <summary>The source broadcaster's user ID for shared chat.</summary>
    [JsonPropertyName("source_broadcaster_user_id")]
    public string? SourceBroadcasterUserId { get; init; }

    /// <summary>The source broadcaster's user login for shared chat.</summary>
    [JsonPropertyName("source_broadcaster_user_login")]
    public string? SourceBroadcasterUserLogin { get; init; }

    /// <summary>The source broadcaster's user display name for shared chat.</summary>
    [JsonPropertyName("source_broadcaster_user_name")]
    public string? SourceBroadcasterUserName { get; init; }

    /// <summary>The source message ID for shared chat.</summary>
    [JsonPropertyName("source_message_id")]
    public string? SourceMessageId { get; init; }

    /// <summary>The source badges for shared chat.</summary>
    [JsonPropertyName("source_badges")]
    public ChatBadgeData[]? SourceBadges { get; init; }
}
