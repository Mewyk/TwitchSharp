using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Settings for the maximum number of redemptions allowed per live stream.
/// </summary>
public sealed record MaxPerStreamSettingData
{
    /// <summary>Whether the reward applies a limit on the number of redemptions allowed per live stream.</summary>
    [JsonPropertyName("is_enabled")]
    public bool IsEnabled { get; init; }

    /// <summary>The maximum number of redemptions allowed per live stream.</summary>
    [JsonPropertyName("max_per_stream")]
    public long MaxPerStream { get; init; }
}
