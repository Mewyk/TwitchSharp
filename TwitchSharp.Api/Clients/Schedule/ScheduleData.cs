using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a broadcaster's streaming schedule.
/// </summary>
public sealed record ScheduleData
{
    /// <summary>The scheduled broadcast segments.</summary>
    [JsonPropertyName("segments")]
    public ScheduleSegmentData[]? Segments { get; init; }

    /// <summary>The broadcaster's ID.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_name")]
    public string BroadcasterName { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_login")]
    public string BroadcasterLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's vacation information, or null if no vacation is scheduled.</summary>
    [JsonPropertyName("vacation")]
    public ScheduleVacationData? Vacation { get; init; }
}
