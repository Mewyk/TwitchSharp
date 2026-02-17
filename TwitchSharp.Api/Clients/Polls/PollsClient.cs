using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Polls API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class PollsClient
{
    private readonly HelixHttpClient _httpClient;

    internal PollsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets a page of polls for the specified broadcaster.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="ids">The IDs of the polls to retrieve.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor for forward pagination.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of polls.</returns>
    public async Task<TwitchPage<PollData>> GetPollsAsync(
        string broadcasterId,
        IEnumerable<string>? ids = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("polls");
        url.Add("broadcaster_id", broadcasterId);
        url.AddRepeated("id", ids);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponsePollData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<PollData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Creates a poll. The poll begins immediately.
    /// </summary>
    /// <param name="request">The poll creation parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The created poll data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<PollData> CreatePollAsync(
        CreatePollRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.CreatePollRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            "polls",
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponsePollData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Create Poll returned no data.");
    }

    /// <summary>
    /// Ends a poll that is currently active.
    /// </summary>
    /// <param name="request">The poll end parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The ended poll data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<PollData> EndPollAsync(
        EndPollRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.EndPollRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Patch,
            "polls",
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponsePollData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("End Poll returned no data.");
    }
}
