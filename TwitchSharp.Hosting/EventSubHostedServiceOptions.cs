namespace TwitchSharp.Hosting;

/// <summary>
/// Options for configuring the <see cref="TwitchEventSubHostedService"/>.
/// </summary>
public sealed class EventSubHostedServiceOptions
{
    /// <summary>
    /// Whether to automatically connect to EventSub on hosted service startup.
    /// Defaults to <c>true</c>.
    /// </summary>
    public bool AutoConnect { get; set; } = true;

    /// <summary>
    /// Subscriptions to automatically create after connecting to EventSub.
    /// </summary>
    public List<EventSubSubscriptionDefinition> Subscriptions { get; set; } = [];
}
