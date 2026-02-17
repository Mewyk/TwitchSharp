using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a fragment within a suspicious user message.
/// The cheermote shape matches <see cref="ChatFragmentCheermoteData"/> and is
/// reused, while the emote uses <see cref="SuspiciousUserFragmentEmoteData"/>
/// because its field set differs from <see cref="ChatFragmentEmoteData"/>.
/// </summary>
public sealed record SuspiciousUserFragmentData
{
    /// <summary>The type of the fragment.</summary>
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
    public SuspiciousUserFragmentEmoteData? Emote { get; init; }
}
