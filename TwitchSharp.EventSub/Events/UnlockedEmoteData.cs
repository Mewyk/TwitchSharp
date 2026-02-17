using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents an emote that was unlocked via an automatic channel points reward redemption.
/// </summary>
public sealed record UnlockedEmoteData
{
    /// <summary>The emote identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The emote name.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}
