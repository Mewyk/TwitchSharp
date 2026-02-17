namespace TwitchSharp.EventSub;

/// <summary>
/// Base type for all EventSub WebSocket messages received by the client.
/// </summary>
public abstract record EventSubMessage
{
    /// <summary>The unique message identifier.</summary>
    public string MessageId { get; init; } = string.Empty;

    /// <summary>The message timestamp in RFC3339 format.</summary>
    public string MessageTimestamp { get; init; } = string.Empty;
}
