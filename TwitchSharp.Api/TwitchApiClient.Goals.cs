using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private GoalsClient? _goals;

    /// <summary>
    /// Gets the Goals API client for managing creator goals.
    /// </summary>
    public GoalsClient Goals => _goals ??= new GoalsClient(_httpClient);
}
