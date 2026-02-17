using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Search API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class SearchClient
{
    private readonly HelixHttpClient _httpClient;

    internal SearchClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Searches for categories/games that match the specified query.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of categories/games matching the query.</returns>
    public async Task<TwitchPage<GameData>> SearchCategoriesAsync(
        string query,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("search/categories");
        url.Add("query", query);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseGameData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<GameData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Searches for channels that match the specified query.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <param name="liveOnly">Whether to return only channels that are currently live.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of channels matching the query.</returns>
    public async Task<TwitchPage<SearchChannelData>> SearchChannelsAsync(
        string query,
        bool? liveOnly = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("search/channels");
        url.Add("query", query);
        url.Add("live_only", liveOnly);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseSearchChannelData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<SearchChannelData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }
}
