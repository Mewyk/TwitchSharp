using TwitchSharp.Api.Clients;

namespace TwitchSharp.EventSub;

/// <summary>
/// Indicates that an EventSub subscription was revoked by Twitch.
/// </summary>
public sealed record EventSubRevocation : EventSubMessage
{
    /// <summary>The subscription that was revoked. Check <c>Status</c> for the reason (authorization_revoked, user_removed, version_removed).</summary>
    public EventSubSubscriptionData Subscription { get; init; } = new();
}
