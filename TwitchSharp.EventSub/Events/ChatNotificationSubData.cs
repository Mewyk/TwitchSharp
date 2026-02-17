using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents subscription data for a chat notification.</summary>
public sealed record ChatNotificationSubData
{
    /// <summary>The subscription tier.</summary>
    [JsonPropertyName("sub_tier")]
    public string SubTier { get; init; } = string.Empty;

    /// <summary>Whether the subscription was through Prime.</summary>
    [JsonPropertyName("is_prime")]
    public bool IsPrime { get; init; }

    /// <summary>The duration of the subscription in months.</summary>
    [JsonPropertyName("duration_months")]
    public int DurationMonths { get; init; }
}
