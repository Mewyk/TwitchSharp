using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a Drop entitlement grant event. This event type is webhook-only and cannot be used with WebSocket.
/// This event uses a batched format with an <see cref="Id"/> for deduplication
/// and a <see cref="Data"/> array containing individual entitlement grants.
/// </summary>
public sealed record DropEntitlementGrantEvent
{
    /// <summary>The individual event identifier for deduplication.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The array of entitlement grant data.</summary>
    [JsonPropertyName("data")]
    public DropEntitlementData[] Data { get; init; } = [];
}
