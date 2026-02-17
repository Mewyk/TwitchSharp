using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a fragment within a user-held message.</summary>
public sealed record UserHeldFragmentData
{
    /// <summary>The text content of the fragment.</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    /// <summary>The emote data if the fragment is an emote.</summary>
    [JsonPropertyName("emote")]
    public UserHeldFragmentEmoteData? Emote { get; init; }

    /// <summary>The cheermote data if the fragment is a cheermote.</summary>
    [JsonPropertyName("cheermote")]
    public ChatFragmentCheermoteData? Cheermote { get; init; }
}
