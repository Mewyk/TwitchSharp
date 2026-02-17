using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Internal;

/// <summary>
/// Internal top-level WebSocket message envelope.
/// </summary>
internal sealed record EventSubWsMessage
{
    [JsonPropertyName("metadata")]
    public EventSubWsMetadata Metadata { get; init; } = new();

    [JsonPropertyName("payload")]
    public EventSubWsPayload Payload { get; init; } = new();
}
