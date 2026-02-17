using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwitchSharp.EventSub;

namespace TwitchSharp.Hosting;

/// <summary>
/// Extension methods for configuring TwitchSharp on <see cref="IHostApplicationBuilder"/>.
/// </summary>
public static class TwitchHostApplicationBuilderExtensions
{
    private const string DefaultConfigSectionPath = "Twitch";

    /// <summary>
    /// Adds TwitchSharp API services, binding options from the "Twitch" configuration section.
    /// </summary>
    /// <param name="builder">The host application builder.</param>
    /// <returns>The builder for chaining.</returns>
    public static IHostApplicationBuilder AddTwitchApi(this IHostApplicationBuilder builder)
    {
        return builder.AddTwitchApi(DefaultConfigSectionPath);
    }

    /// <summary>
    /// Adds TwitchSharp API services, binding options from a custom configuration section.
    /// </summary>
    /// <param name="builder">The host application builder.</param>
    /// <param name="configSectionPath">The configuration section path to bind options from.</param>
    /// <returns>The builder for chaining.</returns>
    public static IHostApplicationBuilder AddTwitchApi(
        this IHostApplicationBuilder builder,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddTwitchApi(builder.Configuration, configSectionPath);

        return builder;
    }

    /// <summary>
    /// Adds the TwitchSharp EventSub WebSocket client.
    /// Requires <see cref="AddTwitchApi(IHostApplicationBuilder)"/> to be called first.
    /// </summary>
    /// <param name="builder">The host application builder.</param>
    /// <param name="configure">Optional action to configure EventSub WebSocket options.</param>
    /// <returns>The builder for chaining.</returns>
    public static IHostApplicationBuilder AddTwitchEventSub(
        this IHostApplicationBuilder builder,
        Action<EventSubWebSocketOptions>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddTwitchEventSub(configure);

        return builder;
    }

    /// <summary>
    /// Adds the TwitchSharp EventSub hosted service that automatically connects, subscribes,
    /// and dispatches messages to registered <see cref="IEventSubHandler"/> implementations.
    /// Calls <see cref="AddTwitchEventSub(IHostApplicationBuilder, Action{EventSubWebSocketOptions}?)"/> if not already registered.
    /// </summary>
    /// <param name="builder">The host application builder.</param>
    /// <param name="configure">Optional action to configure hosted service options.</param>
    /// <returns>The builder for chaining.</returns>
    public static IHostApplicationBuilder AddTwitchEventSubHostedService(
        this IHostApplicationBuilder builder,
        Action<EventSubHostedServiceOptions>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddTwitchEventSubHostedService(configure);

        return builder;
    }
}
