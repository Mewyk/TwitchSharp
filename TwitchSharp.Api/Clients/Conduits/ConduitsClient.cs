using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Conduits API endpoints.
/// </summary>
/// <remarks>All methods may throw <see cref="TwitchApiException"/> on API errors.</remarks>
public sealed class ConduitsClient
{
    private readonly HelixHttpClient _httpClient;

    internal ConduitsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets the conduits for a client ID.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of conduit data.</returns>
    public async Task<ConduitData[]> GetConduitsAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            "eventsub/conduits",
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseConduitData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Creates a new conduit.
    /// </summary>
    /// <param name="request">The conduit creation parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The created conduit data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<ConduitData> CreateConduitAsync(
        CreateConduitRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.CreateConduitRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            "eventsub/conduits",
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseConduitData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Create Conduit returned no data.");
    }

    /// <summary>
    /// Updates a conduit's shard count.
    /// </summary>
    /// <param name="request">The conduit update parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The updated conduit data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<ConduitData> UpdateConduitAsync(
        UpdateConduitRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.UpdateConduitRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Patch,
            "eventsub/conduits",
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseConduitData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Update Conduit returned no data.");
    }

    /// <summary>
    /// Deletes a specified conduit.
    /// </summary>
    /// <param name="conduitId">The ID of the conduit.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task DeleteConduitAsync(
        string conduitId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("eventsub/conduits");
        url.Add("id", conduitId);

        await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a list of all shards for a conduit.
    /// </summary>
    /// <param name="conduitId">The ID of the conduit.</param>
    /// <param name="status">The shard status to filter by.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of conduit shard data.</returns>
    public async Task<TwitchPage<ConduitShardData>> GetConduitShardsAsync(
        string conduitId,
        string? status = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("eventsub/conduits/shards");
        url.Add("conduit_id", conduitId);
        url.Add("status", status);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseConduitShardData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<ConduitShardData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Updates shard(s) for a conduit.
    /// </summary>
    /// <param name="request">The conduit shards update parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The conduit shards update result.</returns>
    public async Task<UpdateConduitShardsResult> UpdateConduitShardsAsync(
        UpdateConduitShardsRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.UpdateConduitShardsRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Patch,
            "eventsub/conduits/shards",
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.UpdateConduitShardsResponse,
            content,
            cancellationToken).ConfigureAwait(false);

        return new UpdateConduitShardsResult
        {
            Data = response.Data ?? [],
            Errors = response.Errors ?? []
        };
    }
}
