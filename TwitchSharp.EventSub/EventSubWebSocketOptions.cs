namespace TwitchSharp.EventSub;

/// <summary>
/// Configuration options for the EventSub WebSocket client.
/// </summary>
public sealed class EventSubWebSocketOptions
{
    /// <summary>
    /// The keepalive timeout in seconds (10-600). If null, the Twitch default is used.
    /// Sent as a query parameter when connecting to the WebSocket.
    /// </summary>
    public int? KeepaliveTimeoutSeconds { get; set; }

    /// <summary>
    /// Whether to automatically reconnect when the connection is lost. Default is true.
    /// </summary>
    public bool AutoReconnect { get; set; } = true;

    /// <summary>
    /// Maximum number of reconnect attempts before giving up. Default is 5.
    /// Only applies to connection-loss reconnection, not server-initiated reconnects.
    /// </summary>
    public int MaxReconnectAttempts { get; set; } = 5;

    /// <summary>
    /// The capacity of the internal message buffer. Default is 1000.
    /// When the buffer is full, the receive loop will apply backpressure.
    /// </summary>
    public int MessageBufferCapacity { get; set; } = 1_000;
}
