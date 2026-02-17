using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private SearchClient? _search;

    /// <summary>
    /// Gets the Search API client for searching Twitch channels and categories.
    /// </summary>
    public SearchClient Search => _search ??= new SearchClient(_httpClient);
}
