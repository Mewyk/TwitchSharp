using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a fragment of a message in a channel points automatic reward redemption event (v2).
/// </summary>
public sealed record AutomaticRewardFragmentData
{
    /// <summary>The fragment type (text, emote).</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The text content of this fragment.</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    /// <summary>The emote data if this fragment is an emote.</summary>
    [JsonPropertyName("emote")]
    public AutomaticRewardFragmentEmoteData? Emote { get; init; }
}
