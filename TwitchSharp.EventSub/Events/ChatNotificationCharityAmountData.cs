using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents the monetary amount for a charity donation.</summary>
public sealed record ChatNotificationCharityAmountData
{
    /// <summary>The monetary value in the minor unit of the currency.</summary>
    [JsonPropertyName("value")]
    public int Value { get; init; }

    /// <summary>The number of decimal places for the currency.</summary>
    [JsonPropertyName("decimal_place")]
    public int DecimalPlace { get; init; }

    /// <summary>The ISO 4217 currency code.</summary>
    [JsonPropertyName("currency")]
    public string Currency { get; init; } = string.Empty;
}
