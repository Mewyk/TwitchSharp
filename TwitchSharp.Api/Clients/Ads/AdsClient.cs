using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Ads API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class AdsClient
{
    private readonly HelixHttpClient _httpClient;

    internal AdsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Starts a commercial on the specified channel.
    /// </summary>
    /// <param name="request">The commercial start parameters including broadcaster ID and duration.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The commercial data including retry delay and message.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<CommercialData> StartCommercialAsync(
        StartCommercialRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.StartCommercialRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            "channels/commercial",
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseCommercialData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Start Commercial returned no data.");
    }

    /// <summary>
    /// Gets the ad schedule for the specified broadcaster.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The ad schedule data, or <see langword="null"/> if not found.</returns>
    public async Task<AdScheduleData?> GetAdScheduleAsync(
        string broadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channels/ads");
        url.Add("broadcaster_id", broadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseAdScheduleData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : null;
    }

    /// <summary>
    /// Snoozes the next ad for the specified broadcaster, pushing it back by 5 minutes.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The snooze data including next ad date and snooze count.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<AdSnoozeData> SnoozeNextAdAsync(
        string broadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channels/ads/schedule/snooze");
        url.Add("broadcaster_id", broadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseAdSnoozeData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Snooze Next Ad returned no data.");
    }
}
