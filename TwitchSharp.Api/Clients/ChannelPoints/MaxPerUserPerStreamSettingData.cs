using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Settings for the maximum number of redemptions allowed per user per live stream.
/// </summary>
public sealed record MaxPerUserPerStreamSettingData
{
    /// <summary>Whether the reward applies a limit on the number of redemptions allowed per user per live stream.</summary>
    [JsonPropertyName("is_enabled")]
    public bool IsEnabled { get; init; }

    /// <summary>The maximum number of redemptions allowed per user per live stream.</summary>
    [JsonPropertyName("max_per_user_per_stream")]
    public long MaxPerUserPerStream { get; init; }
}
