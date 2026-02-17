using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents charity donation data for a chat notification.</summary>
public sealed record ChatNotificationCharityDonationData
{
    /// <summary>The name of the charity.</summary>
    [JsonPropertyName("charity_name")]
    public string CharityName { get; init; } = string.Empty;

    /// <summary>The donation amount details.</summary>
    [JsonPropertyName("amount")]
    public ChatNotificationCharityAmountData Amount { get; init; } = new();
}
