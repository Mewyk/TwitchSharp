using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents the structured message content of a chat message.</summary>
public sealed record ChatMessageData
{
    /// <summary>The text content of the message.</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    /// <summary>The fragments that make up the message.</summary>
    [JsonPropertyName("fragments")]
    public ChatFragmentData[] Fragments { get; init; } = [];
}
