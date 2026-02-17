using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents reply thread data for a chat message.</summary>
public sealed record ChatReplyData
{
    /// <summary>The parent message identifier.</summary>
    [JsonPropertyName("parent_message_id")]
    public string ParentMessageId { get; init; } = string.Empty;

    /// <summary>The body text of the parent message.</summary>
    [JsonPropertyName("parent_message_body")]
    public string ParentMessageBody { get; init; } = string.Empty;

    /// <summary>The parent message author's user ID.</summary>
    [JsonPropertyName("parent_user_id")]
    public string ParentUserId { get; init; } = string.Empty;

    /// <summary>The parent message author's user login.</summary>
    [JsonPropertyName("parent_user_login")]
    public string ParentUserLogin { get; init; } = string.Empty;

    /// <summary>The parent message author's user display name.</summary>
    [JsonPropertyName("parent_user_name")]
    public string ParentUserName { get; init; } = string.Empty;

    /// <summary>The thread root message identifier.</summary>
    [JsonPropertyName("thread_message_id")]
    public string ThreadMessageId { get; init; } = string.Empty;

    /// <summary>The thread root author's user ID.</summary>
    [JsonPropertyName("thread_user_id")]
    public string ThreadUserId { get; init; } = string.Empty;

    /// <summary>The thread root author's user login.</summary>
    [JsonPropertyName("thread_user_login")]
    public string ThreadUserLogin { get; init; } = string.Empty;

    /// <summary>The thread root author's user display name.</summary>
    [JsonPropertyName("thread_user_name")]
    public string ThreadUserName { get; init; } = string.Empty;
}
