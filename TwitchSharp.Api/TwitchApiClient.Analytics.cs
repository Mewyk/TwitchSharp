using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private AnalyticsClient? _analytics;

    /// <summary>
    /// Gets the Analytics API client for retrieving extension and game analytics.
    /// </summary>
    public AnalyticsClient Analytics => _analytics ??= new AnalyticsClient(_httpClient);
}
