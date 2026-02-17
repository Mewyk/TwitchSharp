using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Nested emote data for a Bits power-up.</summary>
public sealed record BitsUsePowerUpEmoteData
{
    /// <summary>The emote identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The human-readable emote token.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}
