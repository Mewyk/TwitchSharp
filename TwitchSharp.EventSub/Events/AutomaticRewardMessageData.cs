using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents the message data for a channel points automatic reward redemption event (v2).
/// </summary>
public sealed record AutomaticRewardMessageData
{
    /// <summary>The message text.</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    /// <summary>The message fragments.</summary>
    [JsonPropertyName("fragments")]
    public AutomaticRewardFragmentData[] Fragments { get; init; } = [];
}
