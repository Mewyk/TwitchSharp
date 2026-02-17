using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a fragment within an AutoMod-held message.</summary>
public sealed record AutomodFragmentData
{
    /// <summary>The type of the fragment (e.g., text, emote, cheermote).</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The text content of the fragment.</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    /// <summary>The cheermote data if the fragment is a cheermote.</summary>
    [JsonPropertyName("cheermote")]
    public ChatFragmentCheermoteData? Cheermote { get; init; }

    /// <summary>The emote data if the fragment is an emote.</summary>
    [JsonPropertyName("emote")]
    public AutomodFragmentEmoteData? Emote { get; init; }
}
