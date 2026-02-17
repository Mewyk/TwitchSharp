using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Nested data for a Bits power-up.</summary>
public sealed record BitsUsePowerUpData
{
    /// <summary>The type of power-up (e.g., message_effect, celebration, gigantify_an_emote).</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The emote associated with the power-up.</summary>
    [JsonPropertyName("emote")]
    public BitsUsePowerUpEmoteData? Emote { get; init; }

    /// <summary>The identifier of the message effect.</summary>
    [JsonPropertyName("message_effect_id")]
    public string? MessageEffectId { get; init; }
}
