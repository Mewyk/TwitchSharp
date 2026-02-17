using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents follower-only mode data within a channel.moderate v2 event.</summary>
public sealed record ModerateFollowersData
{
    /// <summary>The minimum follow duration in minutes required to chat.</summary>
    [JsonPropertyName("follow_duration_minutes")]
    public int FollowDurationMinutes { get; init; }
}
