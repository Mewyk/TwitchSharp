using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TwitchSharp.EventSub;

namespace TwitchSharp.Hosting;

/// <summary>
/// A hosted background service that manages the lifecycle of a <see cref="TwitchEventSubClient"/>,
/// automatically connecting, subscribing, and dispatching messages to registered
/// <see cref="IEventSubHandler"/> implementations.
/// </summary>
public sealed partial class TwitchEventSubHostedService : BackgroundService
{
    private readonly TwitchEventSubClient _eventSubClient;
    private readonly IEnumerable<IEventSubHandler> _handlers;
    private readonly EventSubHostedServiceOptions _options;
    private readonly ILogger<TwitchEventSubHostedService> _logger;

    /// <summary>
    /// Creates a new <see cref="TwitchEventSubHostedService"/>.
    /// </summary>
    /// <param name="eventSubClient">The EventSub WebSocket client.</param>
    /// <param name="handlers">All registered event handlers.</param>
    /// <param name="options">Hosted service options.</param>
    /// <param name="logger">The logger.</param>
    public TwitchEventSubHostedService(
        TwitchEventSubClient eventSubClient,
        IEnumerable<IEventSubHandler> handlers,
        EventSubHostedServiceOptions options,
        ILogger<TwitchEventSubHostedService> logger)
    {
        ArgumentNullException.ThrowIfNull(eventSubClient);
        ArgumentNullException.ThrowIfNull(handlers);
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(logger);

        _eventSubClient = eventSubClient;
        _handlers = handlers;
        _options = options;
        _logger = logger;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_options.AutoConnect)
        {
            LogAutoConnectDisabled();
            return;
        }

        LogStarting();

        try
        {
            await _eventSubClient.ConnectAsync(stoppingToken).ConfigureAwait(false);
            LogConnected();

            await CreateSubscriptionsAsync(stoppingToken).ConfigureAwait(false);

            await foreach (var message in _eventSubClient.ReadAllAsync(stoppingToken).ConfigureAwait(false))
            {
                await DispatchToHandlersAsync(message, stoppingToken).ConfigureAwait(false);
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            // Normal shutdown
        }

        LogStopped();
    }

    /// <inheritdoc />
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        LogStopping();
        await base.StopAsync(cancellationToken).ConfigureAwait(false);
        await _eventSubClient.DisposeAsync().ConfigureAwait(false);
    }

    private async Task CreateSubscriptionsAsync(CancellationToken cancellationToken)
    {
        foreach (var subscription in _options.Subscriptions)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            try
            {
                await _eventSubClient.SubscribeAsync(
                    subscription.Type,
                    subscription.Version,
                    subscription.Condition,
                    cancellationToken).ConfigureAwait(false);

                LogSubscriptionCreated(subscription.Type, subscription.Version);
            }
            catch (Exception exception) when (!cancellationToken.IsCancellationRequested)
            {
                LogSubscriptionFailed(subscription.Type, subscription.Version, exception);
            }
        }
    }

    private async Task DispatchToHandlersAsync(EventSubMessage message, CancellationToken cancellationToken)
    {
        foreach (var handler in _handlers)
        {
            try
            {
                await handler.HandleAsync(message, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception) when (!cancellationToken.IsCancellationRequested)
            {
                LogHandlerError(handler.GetType().Name, exception);
            }
        }
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "EventSub hosted service starting")]
    private partial void LogStarting();

    [LoggerMessage(Level = LogLevel.Information, Message = "EventSub hosted service connected")]
    private partial void LogConnected();

    [LoggerMessage(Level = LogLevel.Information, Message = "EventSub hosted service stopping")]
    private partial void LogStopping();

    [LoggerMessage(Level = LogLevel.Information, Message = "EventSub hosted service stopped")]
    private partial void LogStopped();

    [LoggerMessage(Level = LogLevel.Information, Message = "EventSub auto-connect is disabled, service will not start")]
    private partial void LogAutoConnectDisabled();

    [LoggerMessage(Level = LogLevel.Information, Message = "EventSub auto-subscription created: {Type} v{Version}")]
    private partial void LogSubscriptionCreated(string type, string version);

    [LoggerMessage(Level = LogLevel.Error, Message = "Failed to create EventSub subscription: {Type} v{Version}")]
    private partial void LogSubscriptionFailed(string type, string version, Exception exception);

    [LoggerMessage(Level = LogLevel.Error, Message = "EventSub handler {HandlerName} threw an exception")]
    private partial void LogHandlerError(string handlerName, Exception exception);
}
