using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Nested reward data for a channel points redemption event.</summary>
public sealed record RedemptionRewardData
{
    /// <summary>The reward identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The reward title.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The cost of the reward in channel points.</summary>
    [JsonPropertyName("cost")]
    public int Cost { get; init; }

    /// <summary>The reward prompt text.</summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; init; } = string.Empty;
}
