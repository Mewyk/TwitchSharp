using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents the max-per-user-per-stream setting for a channel points custom reward.
/// </summary>
public sealed record MaxPerUserPerStreamSettingData
{
    /// <summary>Whether the max-per-user-per-stream limit is enabled.</summary>
    [JsonPropertyName("is_enabled")]
    public bool IsEnabled { get; init; }

    /// <summary>The maximum number of redemptions allowed per user per live stream.</summary>
    [JsonPropertyName("max_per_user_per_stream")]
    public int MaxPerUserPerStream { get; init; }
}
