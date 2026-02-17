using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents bits badge tier data for a chat notification.</summary>
public sealed record ChatNotificationBitsBadgeTierData
{
    /// <summary>The bits badge tier achieved.</summary>
    [JsonPropertyName("tier")]
    public int Tier { get; init; }
}
