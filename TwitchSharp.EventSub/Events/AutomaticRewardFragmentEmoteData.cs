using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents emote data within an automatic reward redemption message fragment.
/// </summary>
public sealed record AutomaticRewardFragmentEmoteData
{
    /// <summary>The emote identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;
}
