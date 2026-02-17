using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the cost of an extension Bits product.
/// </summary>
public sealed record ExtensionBitsProductCostData
{
    /// <summary>The product's price.</summary>
    [JsonPropertyName("amount")]
    public int Amount { get; init; }

    /// <summary>The type of currency (e.g., "bits").</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;
}
