using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents an EventSub subscription.
/// </summary>
public sealed record EventSubSubscriptionData
{
    /// <summary>The subscription ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The subscription status.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>The subscription type.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The subscription version.</summary>
    [JsonPropertyName("version")]
    public string Version { get; init; } = string.Empty;

    /// <summary>The subscription condition as key-value pairs.</summary>
    [JsonPropertyName("condition")]
    public Dictionary<string, string>? Condition { get; init; }

    /// <summary>When the subscription was created (RFC3339).</summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = string.Empty;

    /// <summary>The transport configuration.</summary>
    [JsonPropertyName("transport")]
    public EventSubTransportData? Transport { get; init; }

    /// <summary>The cost of the subscription.</summary>
    [JsonPropertyName("cost")]
    public int Cost { get; init; }
}
