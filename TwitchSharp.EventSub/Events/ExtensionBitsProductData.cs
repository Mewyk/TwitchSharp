using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Nested product data for an extension Bits transaction.</summary>
public sealed record ExtensionBitsProductData
{
    /// <summary>The product name.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>The number of Bits involved in the transaction. Will be 0 if in development.</summary>
    [JsonPropertyName("bits")]
    public int Bits { get; init; }

    /// <summary>The unique product identifier (SKU).</summary>
    [JsonPropertyName("sku")]
    public string Sku { get; init; } = string.Empty;

    /// <summary>Whether the product is in development.</summary>
    [JsonPropertyName("in_development")]
    public bool InDevelopment { get; init; }
}
