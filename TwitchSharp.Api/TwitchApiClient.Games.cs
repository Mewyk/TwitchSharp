using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private GamesClient? _games;

    /// <summary>
    /// Gets the Games API client for querying Twitch game/category data.
    /// </summary>
    public GamesClient Games => _games ??= new GamesClient(_httpClient);
}
