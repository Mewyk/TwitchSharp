using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Videos API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class VideosClient
{
    private readonly HelixHttpClient _httpClient;

    internal VideosClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets videos by video IDs, user ID, or game ID.
    /// </summary>
    /// <param name="ids">The IDs of the videos to get.</param>
    /// <param name="userId">The ID of the user whose videos to get.</param>
    /// <param name="gameId">The ID of the game whose videos to get.</param>
    /// <param name="language">The ISO 639-1 language code to filter videos by.</param>
    /// <param name="period">The time period to filter videos by.</param>
    /// <param name="sort">The sort order of the returned videos.</param>
    /// <param name="type">The type of videos to return.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="before">The cursor used to get the previous page of results.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of videos.</returns>
    public async Task<TwitchPage<VideoData>> GetVideosAsync(
        IEnumerable<string>? ids = null,
        string? userId = null,
        string? gameId = null,
        string? language = null,
        string? period = null,
        string? sort = null,
        string? type = null,
        int? first = null,
        string? before = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("videos");
        url.AddRepeated("id", ids);
        url.Add("user_id", userId);
        url.Add("game_id", gameId);
        url.Add("language", language);
        url.Add("period", period);
        url.Add("sort", sort);
        url.Add("type", type);
        url.Add("first", first);
        url.Add("before", before);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseVideoData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<VideoData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Deletes one or more videos. Returns the IDs of successfully deleted videos.
    /// </summary>
    /// <param name="ids">The IDs of the videos to delete.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of IDs of the successfully deleted videos.</returns>
    public async Task<string[]> DeleteVideosAsync(
        IEnumerable<string> ids,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("videos");
        url.AddRepeated("id", ids);

        var response = await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseString,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }
}
