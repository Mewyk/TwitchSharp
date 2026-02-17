using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Channels API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class ChannelsClient
{
    private readonly HelixHttpClient _httpClient;

    internal ChannelsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets channel information for the specified broadcasters.
    /// </summary>
    /// <param name="broadcasterIds">The IDs of the broadcasters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of channel information entries.</returns>
    public async Task<IReadOnlyList<ChannelInformationData>> GetChannelInformationAsync(
        IEnumerable<string> broadcasterIds,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channels");
        url.AddRepeated("broadcaster_id", broadcasterIds);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseChannelInformationData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Modifies channel information for the specified broadcaster.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="request">The channel modification parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task ModifyChannelInformationAsync(
        string broadcasterId,
        ModifyChannelInformationRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channels");
        url.Add("broadcaster_id", broadcasterId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.ModifyChannelInformationRequest);

        await _httpClient.SendAsync(
            HttpMethod.Patch,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            content,
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the list of editors for the specified channel.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of channel editors.</returns>
    public async Task<IReadOnlyList<ChannelEditorData>> GetChannelEditorsAsync(
        string broadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channels/editors");
        url.Add("broadcaster_id", broadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseChannelEditorData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Gets a page of channels that the specified user follows.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="broadcasterId">The ID of the broadcaster to check the follow status for.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of followed channels.</returns>
    public async Task<TwitchPage<FollowedChannelData>> GetFollowedChannelsAsync(
        string userId,
        string? broadcasterId = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channels/followed");
        url.Add("user_id", userId);
        url.Add("broadcaster_id", broadcasterId);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseFollowedChannelData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<FollowedChannelData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Gets a page of followers for the specified broadcaster.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="userId">The ID of the user to check the follow status for.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of channel followers.</returns>
    public async Task<TwitchPage<ChannelFollowerData>> GetChannelFollowersAsync(
        string broadcasterId,
        string? userId = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channels/followers");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("user_id", userId);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseChannelFollowerData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<ChannelFollowerData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }
}
