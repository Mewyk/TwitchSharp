using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Users API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class UsersClient
{
    private readonly HelixHttpClient _httpClient;

    internal UsersClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets information about one or more users by their ID or login name.
    /// If no IDs or logins are specified, the authenticated user is returned.
    /// </summary>
    /// <param name="ids">The IDs of the users to get.</param>
    /// <param name="logins">The login names of the users to get.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of users.</returns>
    public async Task<IReadOnlyList<UserData>> GetUsersAsync(
        IEnumerable<string>? ids = null,
        IEnumerable<string>? logins = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("users");
        url.AddRepeated("id", ids);
        url.AddRepeated("login", logins);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseUserData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Updates the description of the authenticated user.
    /// </summary>
    /// <param name="description">The new description for the user's profile.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The updated user.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<UserData> UpdateUserAsync(
        string? description = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("users");
        url.Add("description", description);

        var response = await _httpClient.SendAsync(
            HttpMethod.Put,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseUserData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Update User returned no data.");
    }

    /// <summary>
    /// Gets a page of users that the specified broadcaster has blocked.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of blocked users.</returns>
    public async Task<TwitchPage<UserBlockData>> GetUserBlockListAsync(
        string broadcasterId,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("users/blocks");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseUserBlockData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<UserBlockData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Blocks the specified user on behalf of the authenticated user.
    /// </summary>
    /// <param name="targetUserId">The ID of the user to block.</param>
    /// <param name="sourceContext">The location where the harassment took place that is causing the block.</param>
    /// <param name="reason">The reason for blocking the user.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task BlockUserAsync(
        string targetUserId,
        string? sourceContext = null,
        string? reason = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("users/blocks");
        url.Add("target_user_id", targetUserId);
        url.Add("source_context", sourceContext);
        url.Add("reason", reason);

        await _httpClient.SendAsync(
            HttpMethod.Put,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Unblocks the specified user on behalf of the authenticated user.
    /// </summary>
    /// <param name="targetUserId">The ID of the user to unblock.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task UnblockUserAsync(
        string targetUserId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("users/blocks");
        url.Add("target_user_id", targetUserId);

        await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a list of all extensions the authenticated user has installed (active and inactive).
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of user extensions.</returns>
    public async Task<IReadOnlyList<UserExtensionData>> GetUserExtensionsAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            "users/extensions/list",
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseUserExtensionData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Gets the active extensions for the specified user or the authenticated user.
    /// </summary>
    /// <param name="userId">The ID of the user. If not specified, the authenticated user is used.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The active extensions data.</returns>
    public async Task<ActiveExtensionsData> GetUserActiveExtensionsAsync(
        string? userId = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("users/extensions");
        url.Add("user_id", userId);

        var authMode = userId is null ? TwitchAuthenticationMode.UserToken : TwitchAuthenticationMode.AppToken;

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            authMode,
            TwitchApiJsonContext.Default.ActiveExtensionsResponse,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? new ActiveExtensionsData();
    }

    /// <summary>
    /// Updates the active extensions for the authenticated user.
    /// </summary>
    /// <param name="extensions">The active extensions parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The updated active extensions data.</returns>
    public async Task<ActiveExtensionsData> UpdateUserExtensionsAsync(
        ActiveExtensionsData extensions,
        CancellationToken cancellationToken = default)
    {
        var payload = new ActiveExtensionsPayload { Data = extensions };
        var content = JsonContent.Create(payload, TwitchApiJsonContext.Default.ActiveExtensionsPayload);

        var response = await _httpClient.SendAsync(
            HttpMethod.Put,
            "users/extensions",
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.ActiveExtensionsResponse,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data ?? new ActiveExtensionsData();
    }
}
