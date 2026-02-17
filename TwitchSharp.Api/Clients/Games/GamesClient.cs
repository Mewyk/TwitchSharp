using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Games API endpoints.
/// </summary>
/// <remarks>All methods may throw <see cref="TwitchApiException"/> on API errors.</remarks>
public sealed class GamesClient
{
    private readonly HelixHttpClient _httpClient;

    internal GamesClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets a page of the most popular games/categories on Twitch.
    /// </summary>
    /// <param name="first">The maximum number of items to return.</param>
    /// <param name="after">The cursor for forward pagination.</param>
    /// <param name="before">The cursor for backward pagination.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of top games.</returns>
    public async Task<TwitchPage<GameData>> GetTopGamesAsync(
        int? first = null,
        string? after = null,
        string? before = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("games/top");
        url.Add("first", first);
        url.Add("after", after);
        url.Add("before", before);

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
    /// Gets information about one or more games/categories by their ID, name, or IGDB ID.
    /// </summary>
    /// <param name="ids">The game IDs to look up.</param>
    /// <param name="names">The game names to look up.</param>
    /// <param name="igdbIds">The IGDB IDs to look up.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of games.</returns>
    public async Task<IReadOnlyList<GameData>> GetGamesAsync(
        IEnumerable<string>? ids = null,
        IEnumerable<string>? names = null,
        IEnumerable<string>? igdbIds = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("games");
        url.AddRepeated("id", ids);
        url.AddRepeated("name", names);
        url.AddRepeated("igdb_id", igdbIds);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseGameData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }
}
