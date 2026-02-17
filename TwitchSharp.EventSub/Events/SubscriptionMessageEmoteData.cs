using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents emote data within a subscription message, including the position
/// of the emote in the message text and its identifier.
/// </summary>
public sealed record SubscriptionMessageEmoteData
{
    /// <summary>The starting index of the emote in the message text.</summary>
    [JsonPropertyName("begin")]
    public int Begin { get; init; }

    /// <summary>The ending index of the emote in the message text.</summary>
    [JsonPropertyName("end")]
    public int End { get; init; }

    /// <summary>The emote identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;
}
