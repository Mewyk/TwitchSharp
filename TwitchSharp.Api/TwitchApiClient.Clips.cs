using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private ClipsClient? _clips;

    /// <summary>
    /// Gets the Clips API client for creating and retrieving clips.
    /// </summary>
    public ClipsClient Clips => _clips ??= new ClipsClient(_httpClient);
}
