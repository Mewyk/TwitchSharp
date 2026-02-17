using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents the structured message content of a user-held message.</summary>
public sealed record UserHeldMessageData
{
    /// <summary>The text content of the message.</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    /// <summary>The fragments that make up the message.</summary>
    [JsonPropertyName("fragments")]
    public UserHeldFragmentData[] Fragments { get; init; } = [];
}
