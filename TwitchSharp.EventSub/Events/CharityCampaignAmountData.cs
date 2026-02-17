using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a monetary amount for charity campaign events.
/// This is separate from <see cref="ChatNotificationCharityAmountData"/> because
/// charity campaign events use <c>decimal_places</c> (plural) while chat notification
/// charity events use <c>decimal_place</c> (singular).
/// </summary>
public sealed record CharityCampaignAmountData
{
    /// <summary>The monetary value in the currency's minor unit (for example, cents for USD).</summary>
    [JsonPropertyName("value")]
    public int Value { get; init; }

    /// <summary>The number of decimal places used by the currency (for example, 2 for USD).</summary>
    [JsonPropertyName("decimal_places")]
    public int DecimalPlaces { get; init; }

    /// <summary>The ISO 4217 three-letter currency code.</summary>
    [JsonPropertyName("currency")]
    public string Currency { get; init; } = string.Empty;
}
