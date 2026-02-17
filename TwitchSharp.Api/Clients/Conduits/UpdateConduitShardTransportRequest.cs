using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Transport configuration for updating a conduit shard.
/// </summary>
public sealed record UpdateConduitShardTransportRequest
{
    /// <summary>The transport method (webhook or websocket).</summary>
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
}
