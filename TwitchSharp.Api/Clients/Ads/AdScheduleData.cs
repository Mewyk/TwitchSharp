using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the ad schedule for a channel.
/// </summary>
public sealed record AdScheduleData
{
    /// <summary>The number of snoozes available for the broadcaster.</summary>
    [JsonPropertyName("snooze_count")]
    public int SnoozeCount { get; init; }

    /// <summary>The UTC timestamp (RFC3339 format) when the broadcaster gains an additional snooze.</summary>
    [JsonPropertyName("snooze_refresh_at")]
    public string SnoozeRefreshAt { get; init; } = string.Empty;

    /// <summary>The UTC timestamp (RFC3339 format) of the next scheduled ad. Empty if no ad scheduled or channel not live.</summary>
    [JsonPropertyName("next_ad_at")]
    public string NextAdAt { get; init; } = string.Empty;

    /// <summary>The length in seconds of the scheduled upcoming ad break.</summary>
    [JsonPropertyName("duration")]
    public int Duration { get; init; }

    /// <summary>The UTC timestamp (RFC3339 format) of the broadcaster's last ad break. Empty if no ad run or channel not live.</summary>
    [JsonPropertyName("last_ad_at")]
    public string LastAdAt { get; init; } = string.Empty;

    /// <summary>The pre-roll free time remaining in seconds. Returns 0 if not pre-roll free.</summary>
    [JsonPropertyName("preroll_free_time")]
    public int PrerollFreeTime { get; init; }
}
