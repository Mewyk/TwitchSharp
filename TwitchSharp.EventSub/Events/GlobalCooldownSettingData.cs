using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents the global cooldown setting for a channel points custom reward.
/// </summary>
public sealed record GlobalCooldownSettingData
{
    /// <summary>Whether the global cooldown is enabled.</summary>
    [JsonPropertyName("is_enabled")]
    public bool IsEnabled { get; init; }

    /// <summary>The cooldown period, in seconds.</summary>
    [JsonPropertyName("global_cooldown_seconds")]
    public int GlobalCooldownSeconds { get; init; }
}
