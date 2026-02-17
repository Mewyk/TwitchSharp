using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Internal;

/// <summary>
/// Internal metadata from a WebSocket message envelope.
/// </summary>
internal sealed record EventSubWsMetadata
{
    [JsonPropertyName("message_id")]
    public string MessageId { get; init; } = string.Empty;

    [JsonPropertyName("message_type")]
    public string MessageType { get; init; } = string.Empty;

    [JsonPropertyName("message_timestamp")]
    public string MessageTimestamp { get; init; } = string.Empty;

    [JsonPropertyName("subscription_type")]
    public string? SubscriptionType { get; init; }

    [JsonPropertyName("subscription_version")]
    public string? SubscriptionVersion { get; init; }
}
