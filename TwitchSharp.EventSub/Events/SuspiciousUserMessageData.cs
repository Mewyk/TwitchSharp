using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents the message data within a channel.suspicious_user.message EventSub event.
/// </summary>
public sealed record SuspiciousUserMessageData
{
    /// <summary>The ID of the message.</summary>
    [JsonPropertyName("message_id")]
    public string MessageId { get; init; } = string.Empty;

    /// <summary>The text content of the message.</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    /// <summary>The ordered list of message fragments.</summary>
    [JsonPropertyName("fragments")]
    public SuspiciousUserFragmentData[] Fragments { get; init; } = [];
}
