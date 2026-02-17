using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private TeamsClient? _teams;

    /// <summary>
    /// Gets the Teams API client for team information.
    /// </summary>
    public TeamsClient Teams => _teams ??= new TeamsClient(_httpClient);
}
