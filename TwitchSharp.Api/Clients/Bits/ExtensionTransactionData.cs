using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents an extension Bits transaction.
/// </summary>
public sealed record ExtensionTransactionData
{
    /// <summary>An ID that identifies the transaction.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The UTC date and time (in RFC3339 format) of the transaction.</summary>
    [JsonPropertyName("timestamp")]
    public string Timestamp { get; init; } = string.Empty;

    /// <summary>The ID of the broadcaster that owns the channel where the transaction occurred.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_login")]
    public string BroadcasterLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_name")]
    public string BroadcasterName { get; init; } = string.Empty;

    /// <summary>The ID of the user that purchased the digital product.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The type of transaction (e.g., BITS_IN_EXTENSION).</summary>
    [JsonPropertyName("product_type")]
    public string ProductType { get; init; } = string.Empty;

    /// <summary>Details about the digital product.</summary>
    [JsonPropertyName("product_data")]
    public ExtensionProductData ProductData { get; init; } = new();
}
