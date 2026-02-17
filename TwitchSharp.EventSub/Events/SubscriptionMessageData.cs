using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents the message data included in a subscription message event,
/// containing the text and any emotes used.
/// </summary>
public sealed record SubscriptionMessageData
{
    /// <summary>The text of the resubscription message.</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    /// <summary>The emotes used in the resubscription message.</summary>
    [JsonPropertyName("emotes")]
    public SubscriptionMessageEmoteData[] Emotes { get; init; } = [];
}
