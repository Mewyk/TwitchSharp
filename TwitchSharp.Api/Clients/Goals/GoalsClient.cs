using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Goals API endpoints.
/// </summary>
/// <remarks>All methods may throw <see cref="TwitchApiException"/> on API errors.</remarks>
public sealed class GoalsClient
{
    private readonly HelixHttpClient _httpClient;

    internal GoalsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets the broadcaster's list of active goals.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of active creator goals.</returns>
    public async Task<GoalData[]> GetCreatorGoalsAsync(
        string broadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("goals");
        url.Add("broadcaster_id", broadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseGoalData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }
}
