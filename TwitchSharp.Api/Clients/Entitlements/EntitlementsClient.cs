using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Entitlements (Drops) API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class EntitlementsClient
{
    private readonly HelixHttpClient _httpClient;

    internal EntitlementsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets a list of drops entitlements.
    /// </summary>
    /// <param name="ids">The IDs of the entitlements to get.</param>
    /// <param name="userId">The ID of the user to get entitlements for.</param>
    /// <param name="gameId">The ID of the game to get entitlements for.</param>
    /// <param name="fulfillmentStatus">The fulfillment status to filter by.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of drops entitlements.</returns>
    public async Task<TwitchPage<DropsEntitlementData>> GetDropsEntitlementsAsync(
        IEnumerable<string>? ids = null,
        string? userId = null,
        string? gameId = null,
        string? fulfillmentStatus = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("entitlements/drops");
        url.AddRepeated("id", ids);
        url.Add("user_id", userId);
        url.Add("game_id", gameId);
        url.Add("fulfillment_status", fulfillmentStatus);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseDropsEntitlementData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<DropsEntitlementData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Updates the fulfillment status of drops entitlements.
    /// </summary>
    /// <param name="request">The update request containing entitlement IDs and fulfillment status.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of updated drops entitlements.</returns>
    public async Task<UpdateDropsEntitlementData[]> UpdateDropsEntitlementsAsync(
        UpdateDropsEntitlementsRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.UpdateDropsEntitlementsRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Patch,
            "entitlements/drops",
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseUpdateDropsEntitlementData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }
}
