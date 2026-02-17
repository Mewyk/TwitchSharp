using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents the structured message content within an AutoMod-held message.</summary>
public sealed record AutomodMessageData
{
    /// <summary>The text content of the message.</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    /// <summary>The fragments that make up the message.</summary>
    [JsonPropertyName("fragments")]
    public AutomodFragmentData[] Fragments { get; init; } = [];
}
