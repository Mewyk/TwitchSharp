using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Teams API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class TeamsClient
{
    private readonly HelixHttpClient _httpClient;

    internal TeamsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets the list of teams that the specified broadcaster is a member of.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of teams the broadcaster belongs to.</returns>
    public async Task<ChannelTeamData[]> GetChannelTeamsAsync(
        string broadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("teams/channel");
        url.Add("broadcaster_id", broadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseChannelTeamData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Gets a team by name or ID.
    /// </summary>
    /// <param name="name">The name of the team.</param>
    /// <param name="id">The ID of the team.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The team, or <see langword="null"/> if not found.</returns>
    public async Task<TeamData?> GetTeamAsync(
        string? name = null,
        string? id = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("teams");
        url.Add("name", name);
        url.Add("id", id);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseTeamData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : null;
    }
}
