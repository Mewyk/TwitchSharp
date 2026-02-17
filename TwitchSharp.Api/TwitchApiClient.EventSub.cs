using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private EventSubClient? _eventSub;

    /// <summary>
    /// Gets the EventSub API client for managing event subscriptions.
    /// </summary>
    public EventSubClient EventSub => _eventSub ??= new EventSubClient(_httpClient);
}
