using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents community subscription gift data for a chat notification.</summary>
public sealed record ChatNotificationCommunitySubGiftData
{
    /// <summary>The community gift identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The total number of subscriptions gifted.</summary>
    [JsonPropertyName("total")]
    public int Total { get; init; }

    /// <summary>The subscription tier.</summary>
    [JsonPropertyName("sub_tier")]
    public string SubTier { get; init; } = string.Empty;

    /// <summary>The cumulative total of gifts given by this user.</summary>
    [JsonPropertyName("cumulative_total")]
    public int? CumulativeTotal { get; init; }
}
