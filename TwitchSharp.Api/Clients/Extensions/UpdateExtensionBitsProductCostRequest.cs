using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Cost configuration for an Update Extension Bits Product request.
/// </summary>
public sealed record UpdateExtensionBitsProductCostRequest
{
    /// <summary>The product's price (1-10000). Required.</summary>
    [JsonPropertyName("amount")]
    public int Amount { get; init; }

    /// <summary>The type of currency (e.g., "bits"). Required.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;
}
