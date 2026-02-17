using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Send Chat Message endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record SendMessageRequest
{
    /// <summary>The broadcaster's user ID. Required.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The sender's user ID. Must match the authenticated user. Required.</summary>
    [JsonPropertyName("sender_id")]
    public string SenderId { get; init; } = string.Empty;

    /// <summary>The chat message to send (max 500 characters). Required.</summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    /// <summary>The ID of the message being replied to.</summary>
    [JsonPropertyName("reply_parent_message_id")]
    public string? ReplyParentMessageId { get; init; }
}
