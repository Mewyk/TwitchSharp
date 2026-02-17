using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents shared chat message deletion data within a channel.moderate v2 event.</summary>
public sealed record ModerateSharedChatDeleteData
{
    /// <summary>The user ID of the user whose message was deleted.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login of the user whose message was deleted.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the user whose message was deleted.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The unique identifier of the deleted message.</summary>
    [JsonPropertyName("message_id")]
    public string MessageId { get; init; } = string.Empty;

    /// <summary>The body text of the deleted message.</summary>
    [JsonPropertyName("message_body")]
    public string MessageBody { get; init; } = string.Empty;
}
