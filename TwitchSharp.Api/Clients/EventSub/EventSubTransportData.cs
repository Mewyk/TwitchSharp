using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents an EventSub subscription's transport configuration.
/// </summary>
public sealed record EventSubTransportData
{
    /// <summary>The transport method (webhook, websocket, or conduit).</summary>
    [JsonPropertyName("method")]
    public string Method { get; init; } = string.Empty;

    /// <summary>The callback URL (webhook only).</summary>
    [JsonPropertyName("callback")]
    public string? Callback { get; init; }

    /// <summary>The WebSocket session ID (websocket only).</summary>
    [JsonPropertyName("session_id")]
    public string? SessionId { get; init; }

    /// <summary>When the WebSocket connected (websocket only).</summary>
    [JsonPropertyName("connected_at")]
    public string? ConnectedAt { get; init; }

    /// <summary>When the WebSocket disconnected (websocket only).</summary>
    [JsonPropertyName("disconnected_at")]
    public string? DisconnectedAt { get; init; }

    /// <summary>The conduit ID (conduit only).</summary>
    [JsonPropertyName("conduit_id")]
    public string? ConduitId { get; init; }
}
