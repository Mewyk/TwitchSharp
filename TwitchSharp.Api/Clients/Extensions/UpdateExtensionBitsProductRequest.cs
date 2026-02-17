using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Update Extension Bits Product endpoint.
/// Adds or updates a Bits product. If the SKU doesn't exist, the product is created.
/// </summary>
[GenerateWithMethods]
public sealed partial record UpdateExtensionBitsProductRequest
{
    /// <summary>The product's SKU, unique within the extension (max 255 chars, alphanumeric/dash/underscore/period). Required.</summary>
    [JsonPropertyName("sku")]
    public string Sku { get; init; } = string.Empty;

    /// <summary>The product's cost information. Required.</summary>
    [JsonPropertyName("cost")]
    public UpdateExtensionBitsProductCostRequest Cost { get; init; } = new();

    /// <summary>The product's display name (max 255 chars). Required.</summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; init; } = string.Empty;

    /// <summary>Whether the product is in development. Defaults to false.</summary>
    [JsonPropertyName("in_development")]
    public bool? InDevelopment { get; init; }

    /// <summary>When the product expires, in RFC3339 format. If not set, the product does not expire.</summary>
    [JsonPropertyName("expiration")]
    public string? Expiration { get; init; }

    /// <summary>Whether Bits product purchase events are broadcast to all extension instances. Defaults to false.</summary>
    [JsonPropertyName("is_broadcast")]
    public bool? IsBroadcast { get; init; }
}
