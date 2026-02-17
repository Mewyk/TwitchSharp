using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a channel.chat.clear_user_messages v1 event.</summary>
public sealed record ChatClearUserMessagesEvent
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

    /// <summary>The target user's ID whose messages were cleared.</summary>
    [JsonPropertyName("target_user_id")]
    public string TargetUserId { get; init; } = string.Empty;

    /// <summary>The target user's login whose messages were cleared.</summary>
    [JsonPropertyName("target_user_login")]
    public string TargetUserLogin { get; init; } = string.Empty;

    /// <summary>The target user's display name whose messages were cleared.</summary>
    [JsonPropertyName("target_user_name")]
    public string TargetUserName { get; init; } = string.Empty;
}
