using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents the max-per-stream setting for a channel points custom reward.
/// </summary>
public sealed record MaxPerStreamSettingData
{
    /// <summary>Whether the max-per-stream limit is enabled.</summary>
    [JsonPropertyName("is_enabled")]
    public bool IsEnabled { get; init; }

    /// <summary>The maximum number of redemptions allowed per live stream.</summary>
    [JsonPropertyName("max_per_stream")]
    public int MaxPerStream { get; init; }
}
