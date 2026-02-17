using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using TwitchSharp.Api;
using TwitchSharp.Api.Clients;
using TwitchSharp.EventSub.Internal;

namespace TwitchSharp.EventSub;

/// <summary>
/// A real-time EventSub WebSocket client that connects to Twitch and delivers event notifications
/// via <see cref="ReadAllAsync"/>. Handles keepalive monitoring, server-initiated reconnects,
/// and automatic re-subscription on connection loss.
/// </summary>
public sealed partial class TwitchEventSubClient : IAsyncDisposable
{
    private static readonly Uri DefaultWebSocketUri = new("wss://eventsub.wss.twitch.tv/ws");

    private readonly TwitchApiClient _apiClient;
    private readonly EventSubWebSocketOptions _options;
    private readonly ILogger _logger;
    private readonly Channel<EventSubMessage> _messageChannel;
    private readonly ConcurrentDictionary<string, SubscriptionRecord> _subscriptions = new();
    private readonly CancellationTokenSource _disposeCts = new();

    private ClientWebSocket? _webSocket;
    private Task? _receiveLoopTask;
    private Task? _keepaliveMonitorTask;
    private long _lastMessageTicks;
    private int _keepaliveTimeoutSeconds;
    private volatile bool _disposed;

    /// <summary>
    /// Creates a new EventSub WebSocket client.
    /// </summary>
    /// <param name="apiClient">The Twitch API client used for subscription management via REST.</param>
    /// <param name="options">Optional configuration. If null, defaults are used.</param>
    /// <param name="loggerFactory">Optional logger factory. If null, logging is disabled.</param>
    public TwitchEventSubClient(TwitchApiClient apiClient, EventSubWebSocketOptions? options = null, ILoggerFactory? loggerFactory = null)
    {
        ArgumentNullException.ThrowIfNull(apiClient);
        _apiClient = apiClient;
        _options = options ?? new EventSubWebSocketOptions();
        _logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<TwitchEventSubClient>();
        _messageChannel = Channel.CreateBounded<EventSubMessage>(
            new BoundedChannelOptions(_options.MessageBufferCapacity)
            {
                SingleWriter = true,
                SingleReader = false,
                FullMode = BoundedChannelFullMode.Wait
            });
    }

    /// <summary>
    /// The current WebSocket session ID, available after <see cref="ConnectAsync"/> completes.
    /// </summary>
    public string? SessionId { get; private set; }

    /// <summary>
    /// Whether the WebSocket connection is currently open.
    /// </summary>
    public bool IsConnected => _webSocket?.State == WebSocketState.Open;

    /// <summary>
    /// Connects to the Twitch EventSub WebSocket and waits for the welcome message.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the client has been disposed.</exception>
    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        LogConnecting();
        var webSocketUri = BuildWebSocketUri(DefaultWebSocketUri);
        var session = await ConnectAndWaitForWelcomeAsync(webSocketUri, cancellationToken).ConfigureAwait(false);

        SessionId = session.Id;
        _keepaliveTimeoutSeconds = session.KeepaliveTimeoutSeconds ?? 10;
        LogConnected(SessionId, _keepaliveTimeoutSeconds);
        Interlocked.Exchange(ref _lastMessageTicks, Environment.TickCount64);

        _receiveLoopTask = RunReceiveLoopAsync(_disposeCts.Token);
        _keepaliveMonitorTask = RunKeepaliveMonitorAsync(_disposeCts.Token);
    }

    /// <summary>
    /// Creates an EventSub subscription for this WebSocket session.
    /// The transport is automatically configured with the current session ID.
    /// </summary>
    /// <param name="type">The subscription type (e.g., "channel.follow").</param>
    /// <param name="version">The subscription version (e.g., "2").</param>
    /// <param name="condition">The subscription condition as key-value pairs.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The created subscription data.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the client has been disposed.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the client is not connected.</exception>
    public async Task<EventSubSubscriptionData> SubscribeAsync(
        string type,
        string version,
        Dictionary<string, string> condition,
        CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (SessionId is null)
        {
            throw new InvalidOperationException("Not connected. Call ConnectAsync first.");
        }

        var request = new CreateEventSubSubscriptionRequest
        {
            Type = type,
            Version = version,
            Condition = condition,
            Transport = new CreateEventSubTransportRequest
            {
                Method = "websocket",
                SessionId = SessionId
            }
        };

        var subscription = await _apiClient.EventSub
            .CreateEventSubSubscriptionAsync(request, cancellationToken)
            .ConfigureAwait(false);

        _subscriptions[subscription.Id] = new SubscriptionRecord(type, version, condition);

        LogSubscriptionCreated(type, subscription.Id);

        return subscription;
    }

    /// <summary>
    /// Deletes an EventSub subscription.
    /// </summary>
    /// <param name="subscriptionId">The subscription ID to delete.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the client has been disposed.</exception>
    public async Task UnsubscribeAsync(string subscriptionId, CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        await _apiClient.EventSub
            .DeleteEventSubSubscriptionAsync(subscriptionId, cancellationToken)
            .ConfigureAwait(false);

        _subscriptions.TryRemove(subscriptionId, out _);

        LogSubscriptionDeleted(subscriptionId);
    }

    /// <summary>
    /// Returns an async enumerable of all EventSub messages (notifications, revocations, and lifecycle events).
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>An async enumerable of <see cref="EventSubMessage"/> instances.</returns>
    public IAsyncEnumerable<EventSubMessage> ReadAllAsync(CancellationToken cancellationToken = default)
    {
        return _messageChannel.Reader.ReadAllAsync(cancellationToken);
    }

    private Uri BuildWebSocketUri(Uri baseUri)
    {
        if (_options.KeepaliveTimeoutSeconds is { } timeout)
        {
            return new Uri($"{baseUri}?keepalive_timeout_seconds={timeout}");
        }
        return baseUri;
    }

    private async Task<EventSubWsSessionData> ConnectAndWaitForWelcomeAsync(
        Uri webSocketUri, CancellationToken cancellationToken)
    {
        var webSocket = new ClientWebSocket();
        try
        {
            await webSocket.ConnectAsync(webSocketUri, cancellationToken).ConfigureAwait(false);

            var message = await ReceiveMessageAsync(webSocket, cancellationToken).ConfigureAwait(false);
            if (message is null || message.Metadata.MessageType != "session_welcome")
            {
                throw new TwitchApiException(
                    "Expected session_welcome message from EventSub WebSocket.",
                    TwitchErrorCodes.Unexpected);
            }

            if (message.Payload.Session is null)
            {
                throw new TwitchApiException(
                    "session_welcome message missing session data.",
                    TwitchErrorCodes.Unexpected);
            }

            // Swap in the new WebSocket
            var previousWebSocket = Interlocked.Exchange(ref _webSocket, webSocket);
            if (previousWebSocket is not null)
            {
                await CloseWebSocketSafelyAsync(previousWebSocket).ConfigureAwait(false);
            }

            return message.Payload.Session;
        }
        catch (Exception)
        {
            webSocket.Dispose();
            throw;
        }
    }

    private async Task RunReceiveLoopAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var webSocket = _webSocket;
                if (webSocket is null || webSocket.State != WebSocketState.Open)
                {
                    break;
                }

                EventSubWsMessage? message;
                try
                {
                    message = await ReceiveMessageAsync(webSocket, cancellationToken).ConfigureAwait(false);
                }
                catch (WebSocketException exception) when (!cancellationToken.IsCancellationRequested)
                {
                    // Connection lost — attempt reconnection
                    LogWebSocketError(exception);
                    await HandleConnectionLossAsync(cancellationToken).ConfigureAwait(false);
                    continue;
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                if (message is null)
                {
                    // WebSocket closed gracefully
                    if (!cancellationToken.IsCancellationRequested && _options.AutoReconnect)
                    {
                        await HandleConnectionLossAsync(cancellationToken).ConfigureAwait(false);
                        continue;
                    }
                    break;
                }

                Interlocked.Exchange(ref _lastMessageTicks, Environment.TickCount64);

                switch (message.Metadata.MessageType)
                {
                    case "session_keepalive":
                        // No action needed — timestamp already updated
                        break;

                    case "notification":
                        if (message.Payload.Subscription is not null)
                        {
                            var notification = new EventSubNotification
                            {
                                MessageId = message.Metadata.MessageId,
                                MessageTimestamp = message.Metadata.MessageTimestamp,
                                SubscriptionType = message.Metadata.SubscriptionType ?? string.Empty,
                                SubscriptionVersion = message.Metadata.SubscriptionVersion ?? string.Empty,
                                Subscription = message.Payload.Subscription,
                                Event = message.Payload.Event
                            };
                            await _messageChannel.Writer.WriteAsync(notification, cancellationToken)
                                .ConfigureAwait(false);
                        }
                        break;

                    case "revocation":
                        if (message.Payload.Subscription is not null)
                        {
                            _subscriptions.TryRemove(message.Payload.Subscription.Id, out _);
                            var revocation = new EventSubRevocation
                            {
                                MessageId = message.Metadata.MessageId,
                                MessageTimestamp = message.Metadata.MessageTimestamp,
                                Subscription = message.Payload.Subscription
                            };
                            await _messageChannel.Writer.WriteAsync(revocation, cancellationToken)
                                .ConfigureAwait(false);
                        }
                        break;

                    case "session_reconnect":
                        if (message.Payload.Session?.ReconnectUrl is { } reconnectUrl)
                        {
                            LogServerReconnectRequested(reconnectUrl);
                            await HandleServerReconnectAsync(new Uri(reconnectUrl), cancellationToken)
                                .ConfigureAwait(false);
                        }
                        break;
                }
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // Normal shutdown
        }
    }

    private async Task RunKeepaliveMonitorAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Check every second
                await Task.Delay(1000, cancellationToken).ConfigureAwait(false);

                var lastTicks = Interlocked.Read(ref _lastMessageTicks);
                if (lastTicks == 0)
                {
                    continue;
                }

                var elapsed = Environment.TickCount64 - lastTicks;
                // Add a buffer of 10% to the timeout to avoid false positives
                var timeoutMs = (long)(_keepaliveTimeoutSeconds * 1100);

                if (elapsed > timeoutMs && _webSocket?.State == WebSocketState.Open)
                {
                    // Connection appears dead — the receive loop will handle reconnection
                    // when the WebSocket operation fails or we abort it
                    LogKeepaliveTimeout();
                    _webSocket?.Abort();
                }
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // Normal shutdown
        }
    }

    private async Task HandleServerReconnectAsync(Uri reconnectUrl, CancellationToken cancellationToken)
    {
        // Connect to the new URL and wait for welcome — subscriptions are preserved
        var session = await ConnectAndWaitForWelcomeAsync(reconnectUrl, cancellationToken)
            .ConfigureAwait(false);

        SessionId = session.Id;
        _keepaliveTimeoutSeconds = session.KeepaliveTimeoutSeconds ?? _keepaliveTimeoutSeconds;
        Interlocked.Exchange(ref _lastMessageTicks, Environment.TickCount64);
    }

    private async Task HandleConnectionLossAsync(CancellationToken cancellationToken)
    {
        if (!_options.AutoReconnect)
        {
            _messageChannel.Writer.TryComplete();
            return;
        }

        for (var attempt = 0; attempt < _options.MaxReconnectAttempts; attempt++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            LogConnectionLost(attempt + 1, _options.MaxReconnectAttempts);

            // Exponential backoff: min(2^attempt * 1s, 30s)
            var delayMs = Math.Min(1000 * (1 << attempt), 30_000);
            await Task.Delay(delayMs, cancellationToken).ConfigureAwait(false);

            try
            {
                var webSocketUri = BuildWebSocketUri(DefaultWebSocketUri);
                var session = await ConnectAndWaitForWelcomeAsync(webSocketUri, cancellationToken)
                    .ConfigureAwait(false);

                SessionId = session.Id;
                _keepaliveTimeoutSeconds = session.KeepaliveTimeoutSeconds ?? 10;
                Interlocked.Exchange(ref _lastMessageTicks, Environment.TickCount64);

                // Re-subscribe all tracked subscriptions with the new session
                await ResubscribeAllAsync(cancellationToken).ConfigureAwait(false);

                LogReconnected();

                // Notify user of reconnection
                var reconnectedMessage = new EventSubSessionReconnected
                {
                    MessageId = Guid.NewGuid().ToString(),
                    MessageTimestamp = DateTimeOffset.UtcNow.ToString("o")
                };
                await _messageChannel.Writer.WriteAsync(reconnectedMessage, cancellationToken)
                    .ConfigureAwait(false);

                return; // Successfully reconnected
            }
            catch (Exception) when (!cancellationToken.IsCancellationRequested)
            {
                // Retry on next iteration
            }
        }

        // All reconnect attempts exhausted
        LogReconnectExhausted(_options.MaxReconnectAttempts);
        _messageChannel.Writer.TryComplete(
            new TwitchApiException(
                $"Failed to reconnect after {_options.MaxReconnectAttempts} attempts.",
                TwitchErrorCodes.NetworkError));
    }

    private async Task ResubscribeAllAsync(CancellationToken cancellationToken)
    {
        // Snapshot the current subscriptions and clear the dictionary
        // (new IDs will be added as we re-subscribe)
        var subscriptionsToRestore = _subscriptions.ToArray();
        _subscriptions.Clear();

        foreach (var (_, record) in subscriptionsToRestore)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            try
            {
                await SubscribeAsync(record.Type, record.Version, record.Condition, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (TwitchApiException)
            {
                // Individual subscription failures during reconnect are non-fatal.
                // The user will notice via missing events or can re-subscribe manually.
            }
        }
    }

    private static async Task<EventSubWsMessage?> ReceiveMessageAsync(
        ClientWebSocket webSocket, CancellationToken cancellationToken)
    {
        var buffer = new byte[4096];
        using var stream = new MemoryStream();

        WebSocketReceiveResult result;
        do
        {
            result = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer),
                cancellationToken).ConfigureAwait(false);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                return null;
            }

            stream.Write(buffer, 0, result.Count);
        }
        while (!result.EndOfMessage);

        stream.Position = 0;
        return JsonSerializer.Deserialize(stream, EventSubWsJsonContext.Default.EventSubWsMessage);
    }

    private static async Task CloseWebSocketSafelyAsync(ClientWebSocket webSocket)
    {
        try
        {
            if (webSocket.State is WebSocketState.Open or WebSocketState.CloseReceived)
            {
                using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, cancellationTokenSource.Token)
                    .ConfigureAwait(false);
            }
        }
        catch (Exception)
        {
            // Best-effort close
        }
        finally
        {
            webSocket.Dispose();
        }
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }
        _disposed = true;

        await _disposeCts.CancelAsync().ConfigureAwait(false);

        _messageChannel.Writer.TryComplete();

        // Wait for background tasks to finish
        if (_receiveLoopTask is not null)
        {
            try { await _receiveLoopTask.ConfigureAwait(false); }
            catch (OperationCanceledException) { }
        }
        if (_keepaliveMonitorTask is not null)
        {
            try { await _keepaliveMonitorTask.ConfigureAwait(false); }
            catch (OperationCanceledException) { }
        }

        var webSocket = Interlocked.Exchange(ref _webSocket, null);
        if (webSocket is not null)
        {
            await CloseWebSocketSafelyAsync(webSocket).ConfigureAwait(false);
        }

        _disposeCts.Dispose();
    }

    /// <summary>
    /// Internal record tracking subscription parameters for re-subscription on reconnection.
    /// </summary>
    private sealed record SubscriptionRecord(
        string Type,
        string Version,
        Dictionary<string, string> Condition);

    [LoggerMessage(Level = LogLevel.Information, Message = "Connecting to Twitch EventSub WebSocket")]
    private partial void LogConnecting();

    [LoggerMessage(Level = LogLevel.Information, Message = "EventSub connected (session: {SessionId}, keepalive: {KeepaliveTimeoutSeconds}s)")]
    private partial void LogConnected(string sessionId, int keepaliveTimeoutSeconds);

    [LoggerMessage(Level = LogLevel.Information, Message = "EventSub subscription created: {Type} (id: {SubscriptionId})")]
    private partial void LogSubscriptionCreated(string type, string subscriptionId);

    [LoggerMessage(Level = LogLevel.Information, Message = "EventSub subscription deleted: {SubscriptionId}")]
    private partial void LogSubscriptionDeleted(string subscriptionId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Server requested reconnect to {ReconnectUrl}")]
    private partial void LogServerReconnectRequested(string reconnectUrl);

    [LoggerMessage(Level = LogLevel.Information, Message = "EventSub reconnected successfully")]
    private partial void LogReconnected();

    [LoggerMessage(Level = LogLevel.Warning, Message = "Keepalive timeout exceeded, aborting connection")]
    private partial void LogKeepaliveTimeout();

    [LoggerMessage(Level = LogLevel.Warning, Message = "Connection lost, reconnecting (attempt {Attempt}/{MaxAttempts})")]
    private partial void LogConnectionLost(int attempt, int maxAttempts);

    [LoggerMessage(Level = LogLevel.Error, Message = "WebSocket error occurred")]
    private partial void LogWebSocketError(Exception exception);

    [LoggerMessage(Level = LogLevel.Error, Message = "Failed to reconnect after {MaxAttempts} attempts")]
    private partial void LogReconnectExhausted(int maxAttempts);
}
