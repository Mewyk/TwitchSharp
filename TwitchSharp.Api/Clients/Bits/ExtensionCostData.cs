using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the cost of an extension digital product.
/// </summary>
public sealed record ExtensionCostData
{
    /// <summary>The amount exchanged for the digital product.</summary>
    [JsonPropertyName("amount")]
    public int Amount { get; init; }

    /// <summary>The type of currency exchanged (e.g., bits).</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;
}
