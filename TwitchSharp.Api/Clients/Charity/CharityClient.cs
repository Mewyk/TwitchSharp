using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Charity API endpoints.
/// </summary>
/// <remarks>All methods may throw <see cref="TwitchApiException"/> on API errors.</remarks>
public sealed class CharityClient
{
    private readonly HelixHttpClient _httpClient;

    internal CharityClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets the broadcaster's active charity campaign.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of charity campaigns.</returns>
    public async Task<CharityCampaignData[]> GetCharityCampaignAsync(
        string broadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("charity/campaigns");
        url.Add("broadcaster_id", broadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseCharityCampaignData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Gets donations to the broadcaster's active charity campaign.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of charity donations.</returns>
    public async Task<TwitchPage<CharityDonationData>> GetCharityCampaignDonationsAsync(
        string broadcasterId,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("charity/donations");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseCharityDonationData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<CharityDonationData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }
}
