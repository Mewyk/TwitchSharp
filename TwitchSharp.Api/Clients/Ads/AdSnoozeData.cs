using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the result of snoozing the next ad.
/// </summary>
public sealed record AdSnoozeData
{
    /// <summary>The number of snoozes available for the broadcaster.</summary>
    [JsonPropertyName("snooze_count")]
    public int SnoozeCount { get; init; }

    /// <summary>The UTC timestamp (RFC3339 format) when the broadcaster gains an additional snooze.</summary>
    [JsonPropertyName("snooze_refresh_at")]
    public string SnoozeRefreshAt { get; init; } = string.Empty;

    /// <summary>The UTC timestamp (RFC3339 format) of the broadcaster's next scheduled ad.</summary>
    [JsonPropertyName("next_ad_at")]
    public string NextAdAt { get; init; } = string.Empty;
}
