using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents resubscription data for a chat notification.</summary>
public sealed record ChatNotificationResubData
{
    /// <summary>The cumulative number of months subscribed.</summary>
    [JsonPropertyName("cumulative_months")]
    public int CumulativeMonths { get; init; }

    /// <summary>The duration of the subscription in months.</summary>
    [JsonPropertyName("duration_months")]
    public int DurationMonths { get; init; }

    /// <summary>The number of consecutive months subscribed.</summary>
    [JsonPropertyName("streak_months")]
    public int StreakMonths { get; init; }

    /// <summary>The subscription tier.</summary>
    [JsonPropertyName("sub_tier")]
    public string SubTier { get; init; } = string.Empty;

    /// <summary>Whether the subscription was through Prime.</summary>
    [JsonPropertyName("is_prime")]
    public bool IsPrime { get; init; }

    /// <summary>Whether the subscription was a gift.</summary>
    [JsonPropertyName("is_gift")]
    public bool IsGift { get; init; }

    /// <summary>Whether the gifter is anonymous.</summary>
    [JsonPropertyName("gifter_is_anonymous")]
    public bool? GifterIsAnonymous { get; init; }

    /// <summary>The gifter's user ID.</summary>
    [JsonPropertyName("gifter_user_id")]
    public string? GifterUserId { get; init; }

    /// <summary>The gifter's user display name.</summary>
    [JsonPropertyName("gifter_user_name")]
    public string? GifterUserName { get; init; }

    /// <summary>The gifter's user login.</summary>
    [JsonPropertyName("gifter_user_login")]
    public string? GifterUserLogin { get; init; }
}
