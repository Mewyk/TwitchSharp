using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private SubscriptionsClient? _subscriptions;

    /// <summary>
    /// Gets the Subscriptions API client for managing Twitch subscription data.
    /// </summary>
    public SubscriptionsClient Subscriptions => _subscriptions ??= new SubscriptionsClient(_httpClient);
}
