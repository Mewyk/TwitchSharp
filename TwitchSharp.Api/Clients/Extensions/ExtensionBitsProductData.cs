using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a Bits product created by an extension.
/// </summary>
public sealed record ExtensionBitsProductData
{
    /// <summary>The product's SKU, unique across the extension's products.</summary>
    [JsonPropertyName("sku")]
    public string Sku { get; init; } = string.Empty;

    /// <summary>The product's cost information.</summary>
    [JsonPropertyName("cost")]
    public ExtensionBitsProductCostData Cost { get; init; } = new();

    /// <summary>Whether the product is in development and not available for public use.</summary>
    [JsonPropertyName("in_development")]
    public bool InDevelopment { get; init; }

    /// <summary>The product's display name in the extension.</summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; init; } = string.Empty;

    /// <summary>When the product expires, in RFC3339 format.</summary>
    [JsonPropertyName("expiration")]
    public string Expiration { get; init; } = string.Empty;

    /// <summary>Whether Bits product purchase events are broadcast to all extension instances on a channel.</summary>
    [JsonPropertyName("is_broadcast")]
    public bool IsBroadcast { get; init; }
}
