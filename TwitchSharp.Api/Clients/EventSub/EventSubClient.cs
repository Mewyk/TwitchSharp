using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix EventSub API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class EventSubClient
{
    private readonly HelixHttpClient _httpClient;

    internal EventSubClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Creates an EventSub subscription.
    /// </summary>
    /// <param name="request">The subscription creation parameters including type, version, condition, and transport.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The created EventSub subscription.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<EventSubSubscriptionData> CreateEventSubSubscriptionAsync(
        CreateEventSubSubscriptionRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.CreateEventSubSubscriptionRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            "eventsub/subscriptions",
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.EventSubResponse,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Create EventSub Subscription returned no data.");
    }

    /// <summary>
    /// Deletes an EventSub subscription.
    /// </summary>
    /// <param name="subscriptionId">The ID of the subscription to delete.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task DeleteEventSubSubscriptionAsync(
        string subscriptionId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("eventsub/subscriptions");
        url.Add("id", subscriptionId);

        await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a list of EventSub subscriptions.
    /// </summary>
    /// <param name="status">The subscription status to filter by.</param>
    /// <param name="type">The subscription type to filter by.</param>
    /// <param name="userId">The ID of the user to filter subscriptions by.</param>
    /// <param name="subscriptionId">The ID of a specific subscription to get.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of EventSub subscriptions.</returns>
    public async Task<TwitchPage<EventSubSubscriptionData>> GetEventSubSubscriptionsAsync(
        string? status = null,
        string? type = null,
        string? userId = null,
        string? subscriptionId = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("eventsub/subscriptions");
        url.Add("status", status);
        url.Add("type", type);
        url.Add("user_id", userId);
        url.Add("subscription_id", subscriptionId);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.EventSubResponse,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<EventSubSubscriptionData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }
}
