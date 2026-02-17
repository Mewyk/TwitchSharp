using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Settings for the cooldown period between redemptions.
/// </summary>
public sealed record GlobalCooldownSettingData
{
    /// <summary>Whether to apply a cooldown period.</summary>
    [JsonPropertyName("is_enabled")]
    public bool IsEnabled { get; init; }

    /// <summary>The cooldown period, in seconds.</summary>
    [JsonPropertyName("global_cooldown_seconds")]
    public long GlobalCooldownSeconds { get; init; }
}
