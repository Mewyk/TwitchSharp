using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Nested emote data for a user-held message fragment.</summary>
public sealed record UserHeldFragmentEmoteData
{
    /// <summary>The emote identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The emote set identifier.</summary>
    [JsonPropertyName("emote_set_id")]
    public string EmoteSetId { get; init; } = string.Empty;
}
