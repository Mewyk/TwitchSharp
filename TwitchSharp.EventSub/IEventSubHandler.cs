namespace TwitchSharp.EventSub;

/// <summary>
/// Handler interface for consuming EventSub messages in DI environments.
/// Register implementations with the DI container and use
/// <c>AddTwitchEventSubHostedService</c> to automatically dispatch messages.
/// </summary>
public interface IEventSubHandler
{
    /// <summary>
    /// Handles an incoming EventSub message (notification, revocation, or lifecycle event).
    /// </summary>
    /// <param name="message">The EventSub message to handle.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task HandleAsync(EventSubMessage message, CancellationToken cancellationToken);
}
