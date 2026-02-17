using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents emote data within a suspicious user message fragment.
/// This type differs from <see cref="ChatFragmentEmoteData"/> because
/// the suspicious user event payload includes only the emote ID and set ID.
/// </summary>
public sealed record SuspiciousUserFragmentEmoteData
{
    /// <summary>The emote identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The emote set identifier.</summary>
    [JsonPropertyName("emote_set_id")]
    public string EmoteSetId { get; init; } = string.Empty;
}
