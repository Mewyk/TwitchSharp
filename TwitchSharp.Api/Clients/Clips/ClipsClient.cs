using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Clips API endpoints.
/// </summary>
/// <remarks>All methods may throw <see cref="TwitchApiException"/> on API errors.</remarks>
public sealed class ClipsClient
{
    private readonly HelixHttpClient _httpClient;

    internal ClipsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Creates a clip from a live broadcast. This is an asynchronous operation.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="title">The title of the clip.</param>
    /// <param name="duration">The length of the clip in seconds.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The created clip data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<CreateClipData> CreateClipAsync(
        string broadcasterId,
        string? title = null,
        double? duration = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("clips");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("title", title);
        url.Add("duration", duration);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseCreateClipData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Create Clip returned no data.");
    }

    /// <summary>
    /// Creates a clip from a broadcaster's VOD. Since a live stream is actively creating a VOD,
    /// this can also be used to create a clip from earlier in the current stream.
    /// The <paramref name="vodOffset"/> indicates where the clip ends; the clip starts at
    /// (<paramref name="vodOffset"/> - <paramref name="duration"/>).
    /// </summary>
    /// <param name="editorId">The user ID of the editor. Must match the user ID in the access token.</param>
    /// <param name="broadcasterId">The user ID of the channel to create the clip for.</param>
    /// <param name="vodId">The ID of the VOD to clip from.</param>
    /// <param name="vodOffset">The offset in the VOD (in seconds) where the clip will end. Must be greater than or equal to duration.</param>
    /// <param name="title">The title of the clip.</param>
    /// <param name="duration">The length of the clip in seconds (5–60, precision 0.1). Defaults to 30.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The created clip data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<CreateClipData> CreateClipFromVodAsync(
        string editorId,
        string broadcasterId,
        string vodId,
        int vodOffset,
        string title,
        double? duration = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("videos/clips");
        url.Add("editor_id", editorId);
        url.Add("broadcaster_id", broadcasterId);
        url.Add("vod_id", vodId);
        url.Add("vod_offset", vodOffset);
        url.Add("title", title);
        url.Add("duration", duration);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseCreateClipData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Create Clip From VOD returned no data.");
    }

    /// <summary>
    /// Gets clips by clip IDs, broadcaster ID, or game ID.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="gameId">The ID of the game.</param>
    /// <param name="ids">The clip IDs to look up.</param>
    /// <param name="startedAt">The start date used to filter clips in RFC 3339 format.</param>
    /// <param name="endedAt">The end date used to filter clips in RFC 3339 format.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="before">The cursor used to get the previous page of results.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="isFeatured">Whether to filter by featured clips only.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of clip data.</returns>
    public async Task<TwitchPage<ClipData>> GetClipsAsync(
        string? broadcasterId = null,
        string? gameId = null,
        IEnumerable<string>? ids = null,
        string? startedAt = null,
        string? endedAt = null,
        int? first = null,
        string? before = null,
        string? after = null,
        bool? isFeatured = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("clips");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("game_id", gameId);
        url.AddRepeated("id", ids);
        url.Add("started_at", startedAt);
        url.Add("ended_at", endedAt);
        url.Add("first", first);
        url.Add("before", before);
        url.Add("after", after);
        url.Add("is_featured", isFeatured);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseClipData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<ClipData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Gets clip download URLs.
    /// </summary>
    /// <param name="editorId">The ID of the editor requesting the download.</param>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="clipIds">The IDs of the clips to get download URLs for.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of clip download data.</returns>
    public async Task<ClipDownloadData[]> GetClipsDownloadAsync(
        string editorId,
        string broadcasterId,
        IEnumerable<string> clipIds,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("clips/downloads");
        url.Add("editor_id", editorId);
        url.Add("broadcaster_id", broadcasterId);
        url.AddRepeated("clip_id", clipIds);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseClipDownloadData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }
}
