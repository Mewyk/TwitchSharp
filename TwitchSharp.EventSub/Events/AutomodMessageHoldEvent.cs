using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents an automod.message.hold v2 event, fired when AutoMod or a blocked term holds a message for review.</summary>
public sealed record AutomodMessageHoldEvent
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

    /// <summary>The user ID of the sender whose message was held.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login of the sender whose message was held.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the sender whose message was held.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The unique identifier of the held message.</summary>
    [JsonPropertyName("message_id")]
    public string MessageId { get; init; } = string.Empty;

    /// <summary>The structured message content that was held.</summary>
    [JsonPropertyName("message")]
    public AutomodMessageData Message { get; init; } = new();

    /// <summary>The reason the message was held (e.g., automod, blocked_term).</summary>
    [JsonPropertyName("reason")]
    public string Reason { get; init; } = string.Empty;

    /// <summary>The AutoMod details if the message was held by AutoMod.</summary>
    [JsonPropertyName("automod")]
    public AutomodDetailsData? Automod { get; init; }

    /// <summary>The blocked term details if the message was held due to a blocked term.</summary>
    [JsonPropertyName("blocked_term")]
    public AutomodBlockedTermData? BlockedTerm { get; init; }

    /// <summary>The UTC timestamp of when the message was held.</summary>
    [JsonPropertyName("held_at")]
    public string HeldAt { get; init; } = string.Empty;
}
