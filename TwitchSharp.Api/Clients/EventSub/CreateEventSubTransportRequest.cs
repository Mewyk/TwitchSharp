using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Transport configuration for creating an EventSub subscription.
/// </summary>
public sealed record CreateEventSubTransportRequest
{
    /// <summary>The transport method (webhook, websocket, or conduit). Required.</summary>
    [JsonPropertyName("method")]
    public string Method { get; init; } = string.Empty;

    /// <summary>The callback URL (required for webhook).</summary>
    [JsonPropertyName("callback")]
    public string? Callback { get; init; }

    /// <summary>The secret for webhook verification (required for webhook, 10-100 ASCII chars).</summary>
    [JsonPropertyName("secret")]
    public string? Secret { get; init; }

    /// <summary>The WebSocket session ID (required for websocket).</summary>
    [JsonPropertyName("session_id")]
    public string? SessionId { get; init; }

    /// <summary>The conduit ID (required for conduit).</summary>
    [JsonPropertyName("conduit_id")]
    public string? ConduitId { get; init; }
}
