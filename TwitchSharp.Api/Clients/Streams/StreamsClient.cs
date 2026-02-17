using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Streams API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class StreamsClient
{
    private readonly HelixHttpClient _httpClient;

    internal StreamsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets the stream key for the specified broadcaster.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The stream key.</returns>
    public async Task<string> GetStreamKeyAsync(
        string broadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("streams/key");
        url.Add("broadcaster_id", broadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseStreamKeyData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0].StreamKey
            : string.Empty;
    }

    /// <summary>
    /// Gets a page of active streams matching the specified filters.
    /// </summary>
    /// <param name="userIds">The IDs of the users whose streams to get.</param>
    /// <param name="userLogins">The login names of the users whose streams to get.</param>
    /// <param name="gameIds">The IDs of the games to filter streams by.</param>
    /// <param name="type">The stream type to filter by (e.g., "live").</param>
    /// <param name="languages">The language codes to filter streams by.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="before">The cursor used to get the previous page of results.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of active streams.</returns>
    public async Task<TwitchPage<StreamData>> GetStreamsAsync(
        IEnumerable<string>? userIds = null,
        IEnumerable<string>? userLogins = null,
        IEnumerable<string>? gameIds = null,
        string? type = null,
        IEnumerable<string>? languages = null,
        int? first = null,
        string? before = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("streams");
        url.AddRepeated("user_id", userIds);
        url.AddRepeated("user_login", userLogins);
        url.AddRepeated("game_id", gameIds);
        url.Add("type", type);
        url.AddRepeated("language", languages);
        url.Add("first", first);
        url.Add("before", before);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseStreamData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<StreamData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Gets a page of streams from channels that the specified user follows.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of followed streams.</returns>
    public async Task<TwitchPage<StreamData>> GetFollowedStreamsAsync(
        string userId,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("streams/followed");
        url.Add("user_id", userId);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseStreamData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<StreamData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Creates a stream marker at the current position in the specified broadcaster's stream.
    /// </summary>
    /// <param name="request">The create stream marker parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The created stream marker.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<StreamMarkerData> CreateStreamMarkerAsync(
        CreateStreamMarkerRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.CreateStreamMarkerRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            "streams/markers",
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseStreamMarkerData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Create Stream Marker returned no data.");
    }

    /// <summary>
    /// Gets a page of stream markers for the specified user or video.
    /// </summary>
    /// <param name="userId">The ID of the user whose stream markers to get.</param>
    /// <param name="videoId">The ID of the video whose stream markers to get.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="before">The cursor used to get the previous page of results.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of stream marker containers.</returns>
    public async Task<TwitchPage<StreamMarkerContainerData>> GetStreamMarkersAsync(
        string? userId = null,
        string? videoId = null,
        int? first = null,
        string? before = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("streams/markers");
        url.Add("user_id", userId);
        url.Add("video_id", videoId);
        url.Add("first", first);
        url.Add("before", before);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseStreamMarkerContainerData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<StreamMarkerContainerData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }
}
