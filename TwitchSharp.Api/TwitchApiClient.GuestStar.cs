using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private GuestStarClient? _guestStar;

    /// <summary>
    /// Gets the Guest Star API client for managing Guest Star sessions, invitations, and slots.
    /// All Guest Star endpoints are in BETA and may change without notice.
    /// </summary>
    public GuestStarClient GuestStar => _guestStar ??= new GuestStarClient(_httpClient);
}
