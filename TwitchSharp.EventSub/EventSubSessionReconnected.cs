namespace TwitchSharp.EventSub;

/// <summary>
/// Indicates that the WebSocket connection was lost and re-established.
/// All tracked subscriptions have been automatically re-created with the new session.
/// Events may have been missed during the reconnection window.
/// </summary>
public sealed record EventSubSessionReconnected : EventSubMessage;
