using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents slow mode data within a channel.moderate v2 event.</summary>
public sealed record ModerateSlowData
{
    /// <summary>The wait time in seconds between messages in slow mode.</summary>
    [JsonPropertyName("wait_time_seconds")]
    public int WaitTimeSeconds { get; init; }
}
