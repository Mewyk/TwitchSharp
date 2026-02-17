using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TwitchSharp.Api;
using TwitchSharp.Api.Authentication;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.RateLimiting;

namespace TwitchSharp.Hosting;

/// <summary>
/// Extension methods for registering TwitchSharp API services into the DI container.
/// </summary>
public static class TwitchApiServiceCollectionExtensions
{
    private const string DefaultConfigSectionPath = "Twitch";

    /// <summary>
    /// Adds TwitchSharp API services, binding options from the "Twitch" configuration section.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddTwitchApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddTwitchApi(configuration, DefaultConfigSectionPath);
    }

    /// <summary>
    /// Adds TwitchSharp API services, binding options from a custom configuration section.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="configSectionPath">The configuration section path to bind options from.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddTwitchApi(
        this IServiceCollection services,
        IConfiguration configuration,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services
            .AddOptions<TwitchApiClientOptions>()
            .BindConfiguration(configSectionPath);

        return services.AddTwitchApiCore();
    }

    /// <summary>
    /// Adds TwitchSharp API services with programmatic options configuration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">The action to configure options.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddTwitchApi(
        this IServiceCollection services,
        Action<TwitchApiClientOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        services.Configure(configure);

        return services.AddTwitchApiCore();
    }

    /// <summary>
    /// Post-configure TwitchSharp API options after initial registration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">The action to configure options.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection ConfigureTwitchApi(
        this IServiceCollection services,
        Action<TwitchApiClientOptions> configure)
    {
        services.PostConfigure(configure);
        return services;
    }

    private static IServiceCollection AddTwitchApiCore(this IServiceCollection services)
    {
        // Options validation
        services.TryAddEnumerable(
            ServiceDescriptor.Singleton<IValidateOptions<TwitchApiClientOptions>, TwitchApiClientOptionsValidator>());

        // Validate on startup
        services
            .AddOptions<TwitchApiClientOptions>()
            .ValidateOnStart();

        // Token manager options (can be configured separately)
        services.TryAddSingleton<TokenManagerOptions>();

        // Token manager
        services.TryAddSingleton(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<TwitchApiClientOptions>>().Value;
            var tokenManagerOptions = serviceProvider.GetRequiredService<TokenManagerOptions>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            return new TwitchTokenManager(options, tokenManagerOptions, loggerFactory);
        });

        // Rate limiter
        services.TryAddSingleton(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<TwitchApiClientOptions>>().Value;
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            return new TwitchRateLimiter(options, loggerFactory);
        });

        // Named HttpClient for Helix API
        services.AddHttpClient(TwitchHttpClientNames.Helix, (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<TwitchApiClientOptions>>().Value;
            client.BaseAddress = TwitchEndpoints.HelixBaseUri;
            client.Timeout = options.RequestTimeout;
        })
        .AddHttpMessageHandler(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<TwitchApiClientOptions>>().Value;
            var tokenManager = serviceProvider.GetRequiredService<TwitchTokenManager>();
            var oauthFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            return new TwitchAuthenticationHandler(
                options,
                tokenManager,
                () => oauthFactory.CreateClient(TwitchHttpClientNames.OAuth));
        })
        .AddHttpMessageHandler(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<TwitchApiClientOptions>>().Value;
            if (options.DisableBuiltInResilience)
            {
                return new PassthroughHandler();
            }
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            return new TwitchResilienceHandler(options, loggerFactory);
        });

        // Named HttpClient for OAuth
        services.AddHttpClient(TwitchHttpClientNames.OAuth, (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<TwitchApiClientOptions>>().Value;
            client.BaseAddress = TwitchEndpoints.OAuthBaseUri;
            client.Timeout = options.RequestTimeout;
        });

        // HelixHttpClient
        services.TryAddSingleton(serviceProvider =>
        {
            var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var rateLimiter = serviceProvider.GetRequiredService<TwitchRateLimiter>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            return new HelixHttpClient(
                () => factory.CreateClient(TwitchHttpClientNames.Helix),
                rateLimiter,
                loggerFactory);
        });

        // TwitchApiClient
        services.TryAddSingleton(serviceProvider =>
        {
            var httpClient = serviceProvider.GetRequiredService<HelixHttpClient>();
            var tokenManager = serviceProvider.GetRequiredService<TwitchTokenManager>();
            var options = serviceProvider.GetRequiredService<IOptions<TwitchApiClientOptions>>().Value;
            var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            return TwitchApiClient.CreateFromComponents(
                httpClient,
                tokenManager,
                options,
                () => factory.CreateClient(TwitchHttpClientNames.OAuth));
        });

        return services;
    }
}
