using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents emote data within a chat fragment.</summary>
public sealed record ChatFragmentEmoteData
{
    /// <summary>The emote identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The emote set identifier.</summary>
    [JsonPropertyName("emote_set_id")]
    public string EmoteSetId { get; init; } = string.Empty;

    /// <summary>The owner identifier of the emote.</summary>
    [JsonPropertyName("owner_id")]
    public string OwnerId { get; init; } = string.Empty;

    /// <summary>The available formats for the emote.</summary>
    [JsonPropertyName("format")]
    public string[] Format { get; init; } = [];
}
