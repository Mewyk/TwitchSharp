using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the product data within an extension Bits transaction.
/// </summary>
/// <remarks>
/// The Twitch API returns these fields in camelCase rather than snake_case.
/// </remarks>
public sealed record ExtensionProductData
{
    /// <summary>An ID that identifies the digital product.</summary>
    [JsonPropertyName("sku")]
    public string Sku { get; init; } = string.Empty;

    /// <summary>Set to twitch.ext. + the extension's ID.</summary>
    [JsonPropertyName("domain")]
    public string Domain { get; init; } = string.Empty;

    /// <summary>Details about the digital product's cost.</summary>
    [JsonPropertyName("cost")]
    public ExtensionCostData Cost { get; init; } = new();

    /// <summary>Whether the product is in development and cannot be exchanged.</summary>
    [JsonPropertyName("inDevelopment")]
    public bool InDevelopment { get; init; }

    /// <summary>The name of the digital product.</summary>
    [JsonPropertyName("displayName")]
    public string DisplayName { get; init; } = string.Empty;

    /// <summary>Always empty since you may purchase only unexpired products.</summary>
    [JsonPropertyName("expiration")]
    public string Expiration { get; init; } = string.Empty;

    /// <summary>Whether the data was broadcast to all instances of the extension.</summary>
    [JsonPropertyName("broadcast")]
    public bool Broadcast { get; init; }
}
