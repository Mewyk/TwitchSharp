using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Bits API endpoints.
/// </summary>
/// <remarks>
/// Methods in this class may throw <see cref="TwitchApiException"/> if the API request fails.
/// </remarks>
public sealed class BitsClient
{
    private readonly HelixHttpClient _httpClient;

    internal BitsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets the Bits leaderboard for the authenticated broadcaster.
    /// </summary>
    /// <param name="count">The number of results to return.</param>
    /// <param name="period">The time period over which data is aggregated.</param>
    /// <param name="startedAt">The start date in RFC 3339 format used with the period.</param>
    /// <param name="userId">The ID of the user whose results to get.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The Bits leaderboard results.</returns>
    public async Task<BitsLeaderboardResult> GetBitsLeaderboardAsync(
        int? count = null,
        string? period = null,
        string? startedAt = null,
        string? userId = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("bits/leaderboard");
        url.Add("count", count);
        url.Add("period", period);
        url.Add("started_at", startedAt);
        url.Add("user_id", userId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseBitsLeaderboardEntryData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new BitsLeaderboardResult
        {
            Entries = response.Data ?? [],
            DateRangeStartedAt = response.DateRange?.StartedAt ?? string.Empty,
            DateRangeEndedAt = response.DateRange?.EndedAt ?? string.Empty,
            Total = response.Total ?? 0
        };
    }

    /// <summary>
    /// Gets the list of Cheermotes that users can use to cheer Bits.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster. If not specified, returns global Cheermotes.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of Cheermotes.</returns>
    public async Task<CheermoteData[]> GetCheermotesAsync(
        string? broadcasterId = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("bits/cheermotes");
        url.Add("broadcaster_id", broadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseCheermoteData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Gets the list of extension transactions for the specified extension.
    /// </summary>
    /// <param name="extensionId">The ID of the extension whose transactions to get.</param>
    /// <param name="ids">Optional transaction IDs to filter by (max 100).</param>
    /// <param name="first">The maximum number of items to return per page (1-100, default 20).</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of extension transaction data.</returns>
    public async Task<TwitchPage<ExtensionTransactionData>> GetExtensionTransactionsAsync(
        string extensionId,
        IEnumerable<string>? ids = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("extensions/transactions");
        url.Add("extension_id", extensionId);
        url.AddRepeated("id", ids);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseExtensionTransactionData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<ExtensionTransactionData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }
}
