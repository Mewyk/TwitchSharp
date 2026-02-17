using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Channel Points API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class ChannelPointsClient
{
    private readonly HelixHttpClient _httpClient;

    internal ChannelPointsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Creates a custom reward in the broadcaster's channel.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="request">The custom reward creation parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The custom reward.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<CustomRewardData> CreateCustomRewardAsync(
        string broadcasterId,
        CreateCustomRewardRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channel_points/custom_rewards");
        url.Add("broadcaster_id", broadcasterId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.CreateCustomRewardRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseCustomRewardData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Create Custom Reward returned no data.");
    }

    /// <summary>
    /// Deletes a custom reward that the broadcaster created.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="rewardId">The ID of the custom reward.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task DeleteCustomRewardAsync(
        string broadcasterId,
        string rewardId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channel_points/custom_rewards");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("id", rewardId);

        await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a list of custom rewards that the specified broadcaster created.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="ids">Optional reward or redemption IDs to filter by.</param>
    /// <param name="onlyManageableRewards">If true, returns only rewards the app can manage.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of custom rewards.</returns>
    public async Task<CustomRewardData[]> GetCustomRewardsAsync(
        string broadcasterId,
        IEnumerable<string>? ids = null,
        bool? onlyManageableRewards = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channel_points/custom_rewards");
        url.Add("broadcaster_id", broadcasterId);
        url.AddRepeated("id", ids);
        url.Add("only_manageable_rewards", onlyManageableRewards);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseCustomRewardData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Gets a list of redemptions for the specified custom reward.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="rewardId">The ID of the custom reward.</param>
    /// <param name="status">The redemption status to filter by.</param>
    /// <param name="ids">Optional reward or redemption IDs to filter by.</param>
    /// <param name="sort">The sort order of the redemptions.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of custom reward redemptions.</returns>
    public async Task<TwitchPage<RedemptionData>> GetCustomRewardRedemptionsAsync(
        string broadcasterId,
        string rewardId,
        string? status = null,
        IEnumerable<string>? ids = null,
        string? sort = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channel_points/custom_rewards/redemptions");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("reward_id", rewardId);
        url.Add("status", status);
        url.AddRepeated("id", ids);
        url.Add("sort", sort);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseRedemptionData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<RedemptionData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Updates a custom reward that the broadcaster created.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="rewardId">The ID of the custom reward.</param>
    /// <param name="request">The custom reward update parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The updated custom reward.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<CustomRewardData> UpdateCustomRewardAsync(
        string broadcasterId,
        string rewardId,
        UpdateCustomRewardRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channel_points/custom_rewards");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("id", rewardId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.UpdateCustomRewardRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Patch,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseCustomRewardData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Update Custom Reward returned no data.");
    }

    /// <summary>
    /// Updates a redemption's status.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="rewardId">The ID of the custom reward.</param>
    /// <param name="redemptionIds">The IDs of the redemptions to update.</param>
    /// <param name="request">The redemption status update parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of updated redemptions.</returns>
    public async Task<RedemptionData[]> UpdateRedemptionStatusAsync(
        string broadcasterId,
        string rewardId,
        IEnumerable<string> redemptionIds,
        UpdateRedemptionStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channel_points/custom_rewards/redemptions");
        url.AddRepeated("id", redemptionIds);
        url.Add("broadcaster_id", broadcasterId);
        url.Add("reward_id", rewardId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.UpdateRedemptionStatusRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Patch,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseRedemptionData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }
}
