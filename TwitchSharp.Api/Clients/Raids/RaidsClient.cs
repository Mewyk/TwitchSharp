using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Raids API endpoints.
/// </summary>
/// <remarks>All methods may throw <see cref="TwitchApiException"/> on API errors.</remarks>
public sealed class RaidsClient
{
    private readonly HelixHttpClient _httpClient;

    internal RaidsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Starts a raid from one broadcaster to another.
    /// </summary>
    /// <param name="fromBroadcasterId">The ID of the broadcaster that is sending the raid.</param>
    /// <param name="toBroadcasterId">The ID of the broadcaster that is receiving the raid.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The raid data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<RaidData> StartRaidAsync(
        string fromBroadcasterId,
        string toBroadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("raids");
        url.Add("from_broadcaster_id", fromBroadcasterId);
        url.Add("to_broadcaster_id", toBroadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseRaidData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Start Raid returned no data.");
    }

    /// <summary>
    /// Cancels a pending raid.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task CancelRaidAsync(
        string broadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("raids");
        url.Add("broadcaster_id", broadcasterId);

        await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
