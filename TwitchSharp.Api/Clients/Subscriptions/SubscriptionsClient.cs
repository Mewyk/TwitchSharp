using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Subscriptions API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class SubscriptionsClient
{
    private readonly HelixHttpClient _httpClient;

    internal SubscriptionsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets a page of subscriptions for the specified broadcaster.
    /// The response includes the total subscriber count and subscriber point total.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="userIds">The IDs of the users to check for subscriptions.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="before">The cursor used to get the previous page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of subscriptions along with the total subscriber count and subscriber point total.</returns>
    public async Task<(TwitchPage<SubscriptionData> Page, int? Total, int? Points)> GetBroadcasterSubscriptionsAsync(
        string broadcasterId,
        IEnumerable<string>? userIds = null,
        int? first = null,
        string? after = null,
        string? before = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("subscriptions");
        url.Add("broadcaster_id", broadcasterId);
        url.AddRepeated("user_id", userIds);
        url.Add("first", first);
        url.Add("after", after);
        url.Add("before", before);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseSubscriptionData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        var page = new TwitchPage<SubscriptionData>(
            response.Data ?? [],
            response.Pagination?.Cursor);

        return (page, response.Total, response.Points);
    }

    /// <summary>
    /// Checks whether the specified user is subscribed to the specified broadcaster.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The user subscription data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<UserSubscriptionData> CheckUserSubscriptionAsync(
        string broadcasterId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("subscriptions/user");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("user_id", userId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseUserSubscriptionData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Check User Subscription returned no data.");
    }
}
