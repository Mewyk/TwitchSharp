using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Analytics API endpoints.
/// </summary>
/// <remarks>
/// Methods in this class may throw <see cref="TwitchApiException"/> if the API request fails.
/// </remarks>
public sealed class AnalyticsClient
{
    private readonly HelixHttpClient _httpClient;

    internal AnalyticsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets analytics for the specified extension or all extensions owned by the authenticated user.
    /// </summary>
    /// <param name="extensionId">The ID of the extension to get analytics for. If not specified, returns analytics for all extensions.</param>
    /// <param name="type">The type of analytics report. If not specified, returns all report types.</param>
    /// <param name="startedAt">The start date for the report in RFC 3339 format.</param>
    /// <param name="endedAt">The end date for the report in RFC 3339 format.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of extension analytics data.</returns>
    public async Task<TwitchPage<ExtensionAnalyticsData>> GetExtensionAnalyticsAsync(
        string? extensionId = null,
        string? type = null,
        string? startedAt = null,
        string? endedAt = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("analytics/extensions");
        url.Add("extension_id", extensionId);
        url.Add("type", type);
        url.Add("started_at", startedAt);
        url.Add("ended_at", endedAt);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseExtensionAnalyticsData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<ExtensionAnalyticsData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Gets analytics for the specified game or all games owned by the authenticated user.
    /// </summary>
    /// <param name="gameId">The ID of the game to get analytics for. If not specified, returns analytics for all games.</param>
    /// <param name="type">The type of analytics report. If not specified, returns all report types.</param>
    /// <param name="startedAt">The start date for the report in RFC 3339 format.</param>
    /// <param name="endedAt">The end date for the report in RFC 3339 format.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of game analytics data.</returns>
    public async Task<TwitchPage<GameAnalyticsData>> GetGameAnalyticsAsync(
        string? gameId = null,
        string? type = null,
        string? startedAt = null,
        string? endedAt = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("analytics/games");
        url.Add("game_id", gameId);
        url.Add("type", type);
        url.Add("started_at", startedAt);
        url.Add("ended_at", endedAt);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseGameAnalyticsData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<GameAnalyticsData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }
}
