using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using TwitchSharp.Api;
using TwitchSharp.EventSub;
using Microsoft.Extensions.Hosting;

namespace TwitchSharp.Hosting;

/// <summary>
/// Extension methods for registering TwitchSharp EventSub WebSocket services into the DI container.
/// </summary>
public static class TwitchEventSubServiceCollectionExtensions
{
    /// <summary>
    /// Adds the TwitchSharp EventSub WebSocket client to the service collection.
    /// Requires <see cref="TwitchApiServiceCollectionExtensions.AddTwitchApi(IServiceCollection, Action{TwitchApiClientOptions})"/>
    /// to be called first.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">Optional action to configure EventSub WebSocket options.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddTwitchEventSub(
        this IServiceCollection services,
        Action<EventSubWebSocketOptions>? configure = null)
    {
        if (configure is not null)
        {
            services.Configure(configure);
        }

        services.TryAddSingleton<EventSubWebSocketOptions>(serviceProvider =>
        {
            var options = new EventSubWebSocketOptions();
            configure?.Invoke(options);
            return options;
        });

        services.TryAddSingleton(serviceProvider =>
        {
            var apiClient = serviceProvider.GetRequiredService<TwitchApiClient>();
            var options = serviceProvider.GetRequiredService<EventSubWebSocketOptions>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            return new TwitchEventSubClient(apiClient, options, loggerFactory);
        });

        return services;
    }

    /// <summary>
    /// Adds the TwitchSharp EventSub hosted service that automatically connects, subscribes,
    /// and dispatches messages to registered <see cref="IEventSubHandler"/> implementations.
    /// Calls <see cref="AddTwitchEventSub"/> if not already registered.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">Optional action to configure hosted service options.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddTwitchEventSubHostedService(
        this IServiceCollection services,
        Action<EventSubHostedServiceOptions>? configure = null)
    {
        services.AddTwitchEventSub();

        var options = new EventSubHostedServiceOptions();
        configure?.Invoke(options);

        services.TryAddSingleton(options);
        services.AddHostedService<TwitchEventSubHostedService>();

        return services;
    }
}
