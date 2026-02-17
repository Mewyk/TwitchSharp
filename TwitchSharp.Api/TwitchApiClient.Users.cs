using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private UsersClient? _users;

    /// <summary>
    /// Gets the Users API client for managing Twitch user data.
    /// </summary>
    public UsersClient Users => _users ??= new UsersClient(_httpClient);
}
