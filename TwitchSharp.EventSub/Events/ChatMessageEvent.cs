using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a channel.chat.message v1 event.</summary>
public sealed record ChatMessageEvent
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

    /// <summary>The unique message identifier.</summary>
    [JsonPropertyName("message_id")]
    public string MessageId { get; init; } = string.Empty;

    /// <summary>The structured message content.</summary>
    [JsonPropertyName("message")]
    public ChatMessageData Message { get; init; } = new();

    /// <summary>The type of message.</summary>
    [JsonPropertyName("message_type")]
    public string MessageType { get; init; } = string.Empty;

    /// <summary>The badges associated with the chatter.</summary>
    [JsonPropertyName("badges")]
    public ChatBadgeData[] Badges { get; init; } = [];

    /// <summary>The cheer data if the message includes bits.</summary>
    [JsonPropertyName("cheer")]
    public ChatCheerData? Cheer { get; init; }

    /// <summary>The color of the chatter's name.</summary>
    [JsonPropertyName("color")]
    public string Color { get; init; } = string.Empty;

    /// <summary>The reply data if the message is a reply.</summary>
    [JsonPropertyName("reply")]
    public ChatReplyData? Reply { get; init; }

    /// <summary>The channel points custom reward ID if redeemed.</summary>
    [JsonPropertyName("channel_points_custom_reward_id")]
    public string? ChannelPointsCustomRewardId { get; init; }

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

    /// <summary>Whether the message is only sent to the source channel during shared chat.</summary>
    [JsonPropertyName("is_source_only")]
    public bool? IsSourceOnly { get; init; }
}
