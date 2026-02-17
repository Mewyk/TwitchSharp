using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a monetary amount in a charity context.
/// </summary>
public sealed record CharityAmountData
{
    /// <summary>The monetary amount in the minor unit of the currency (e.g., cents).</summary>
    [JsonPropertyName("value")]
    public int Value { get; init; }

    /// <summary>The number of decimal places used by the currency.</summary>
    [JsonPropertyName("decimal_places")]
    public int DecimalPlaces { get; init; }

    /// <summary>The ISO 4217 three-letter currency code.</summary>
    [JsonPropertyName("currency")]
    public string Currency { get; init; } = string.Empty;
}
