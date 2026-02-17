using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents the transport configuration for a conduit shard.</summary>
public sealed record ConduitShardTransportData
{
    /// <summary>The transport method, such as webhook or websocket.</summary>
    [JsonPropertyName("method")]
    public string Method { get; init; } = string.Empty;

    /// <summary>The callback URL for webhook transport.</summary>
    [JsonPropertyName("callback")]
    public string Callback { get; init; } = string.Empty;

    /// <summary>The session ID for WebSocket transport.</summary>
    [JsonPropertyName("session_id")]
    public string SessionId { get; init; } = string.Empty;

    /// <summary>The UTC timestamp of when the transport was connected.</summary>
    [JsonPropertyName("connected_at")]
    public string ConnectedAt { get; init; } = string.Empty;

    /// <summary>The UTC timestamp of when the transport was disconnected.</summary>
    [JsonPropertyName("disconnected_at")]
    public string DisconnectedAt { get; init; } = string.Empty;
}
