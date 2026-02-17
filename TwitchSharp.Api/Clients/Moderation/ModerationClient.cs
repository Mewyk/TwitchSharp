using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Moderation API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class ModerationClient
{
    private readonly HelixHttpClient _httpClient;

    internal ModerationClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    // ── AutoMod ──────────────────────────────────────────────────────

    /// <summary>
    /// Checks whether AutoMod would hold the specified messages for review.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="request">The AutoMod status check parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of AutoMod status results.</returns>
    public async Task<AutoModStatusData[]> CheckAutoModStatusAsync(
        string broadcasterId,
        CheckAutoModStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/enforcements/status");
        url.Add("broadcaster_id", broadcasterId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.CheckAutoModStatusRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseAutoModStatusData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Allows or denies a message that was held for review by AutoMod.
    /// </summary>
    /// <param name="request">The AutoMod message management parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task ManageHeldAutoModMessageAsync(
        ManageAutoModMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.ManageAutoModMessageRequest);

        await _httpClient.SendAsync(
            HttpMethod.Post,
            "moderation/automod/message",
            TwitchAuthenticationMode.UserToken,
            content: content,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the broadcaster's AutoMod settings.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of AutoMod settings.</returns>
    public async Task<AutoModSettingsData[]> GetAutoModSettingsAsync(
        string broadcasterId,
        string moderatorId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/automod/settings");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseAutoModSettingsData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Updates the broadcaster's AutoMod settings.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="request">The AutoMod settings update parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of updated AutoMod settings.</returns>
    public async Task<AutoModSettingsData[]> UpdateAutoModSettingsAsync(
        string broadcasterId,
        string moderatorId,
        UpdateAutoModSettingsRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/automod/settings");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.UpdateAutoModSettingsRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Put,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseAutoModSettingsData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    // ── Bans ─────────────────────────────────────────────────────────

    /// <summary>
    /// Gets a list of users that are banned or timed out in the broadcaster's channel.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="userIds">The IDs of the users to filter by.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor for forward pagination.</param>
    /// <param name="before">The cursor for backward pagination.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of banned users.</returns>
    public async Task<TwitchPage<BannedUserData>> GetBannedUsersAsync(
        string broadcasterId,
        IEnumerable<string>? userIds = null,
        int? first = null,
        string? after = null,
        string? before = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/banned");
        url.Add("broadcaster_id", broadcasterId);
        url.AddRepeated("user_id", userIds);
        url.Add("first", first);
        url.Add("after", after);
        url.Add("before", before);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseBannedUserData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<BannedUserData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Bans a user or puts them in a timeout in the broadcaster's channel.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="request">The ban parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The ban response data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<BanResponseData> BanUserAsync(
        string broadcasterId,
        string moderatorId,
        BanUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/bans");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.BanUserRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseBanResponseData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Ban User returned no data.");
    }

    /// <summary>
    /// Removes a ban or timeout from the specified user.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task UnbanUserAsync(
        string broadcasterId,
        string moderatorId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/bans");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("user_id", userId);

        await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    // ── Unban Requests ───────────────────────────────────────────────

    /// <summary>
    /// Gets a list of unban requests for the broadcaster's channel.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="status">The status of the unban requests to filter by.</param>
    /// <param name="userId">The ID of the user to filter by.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor for forward pagination.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of unban requests.</returns>
    public async Task<TwitchPage<UnbanRequestData>> GetUnbanRequestsAsync(
        string broadcasterId,
        string moderatorId,
        string status,
        string? userId = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/unban_requests");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("status", status);
        url.Add("user_id", userId);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseUnbanRequestData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<UnbanRequestData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Resolves an unban request by approving or denying it.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="unbanRequestId">The ID of the unban request to resolve.</param>
    /// <param name="status">The resolution status to apply.</param>
    /// <param name="resolutionText">The optional resolution text to include.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The resolved unban request data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<UnbanRequestData> ResolveUnbanRequestAsync(
        string broadcasterId,
        string moderatorId,
        string unbanRequestId,
        string status,
        string? resolutionText = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/unban_requests");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("unban_request_id", unbanRequestId);
        url.Add("status", status);
        url.Add("resolution_text", resolutionText);

        var response = await _httpClient.SendAsync(
            HttpMethod.Patch,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseUnbanRequestData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Resolve Unban Request returned no data.");
    }

    // ── Blocked Terms ────────────────────────────────────────────────

    /// <summary>
    /// Gets the broadcaster's list of blocked terms.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor for forward pagination.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of blocked terms.</returns>
    public async Task<TwitchPage<BlockedTermData>> GetBlockedTermsAsync(
        string broadcasterId,
        string moderatorId,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/blocked_terms");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseBlockedTermData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<BlockedTermData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Adds a word or phrase to the broadcaster's blocked terms list.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="request">The blocked term creation parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The blocked term data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<BlockedTermData> AddBlockedTermAsync(
        string broadcasterId,
        string moderatorId,
        AddBlockedTermRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/blocked_terms");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.AddBlockedTermRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseBlockedTermData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Add Blocked Term returned no data.");
    }

    /// <summary>
    /// Removes a blocked term from the broadcaster's list.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="termId">The ID of the blocked term to remove.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task RemoveBlockedTermAsync(
        string broadcasterId,
        string moderatorId,
        string termId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/blocked_terms");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("id", termId);

        await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    // ── Chat Messages ────────────────────────────────────────────────

    /// <summary>
    /// Deletes one or all chat messages in the broadcaster's chat room.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="messageId">The ID of the message to delete, or <see langword="null"/> to delete all messages.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task DeleteChatMessagesAsync(
        string broadcasterId,
        string moderatorId,
        string? messageId = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/chat");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("message_id", messageId);

        await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    // ── Moderated Channels ───────────────────────────────────────────

    /// <summary>
    /// Gets a list of channels that the specified user has moderator privileges in.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor for forward pagination.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of moderated channels.</returns>
    public async Task<TwitchPage<ModeratedChannelData>> GetModeratedChannelsAsync(
        string userId,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/channels");
        url.Add("user_id", userId);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseModeratedChannelData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<ModeratedChannelData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    // ── Moderators ───────────────────────────────────────────────────

    /// <summary>
    /// Gets a list of the broadcaster's moderators.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="userIds">The IDs of the users to filter by.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor for forward pagination.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of moderators.</returns>
    public async Task<TwitchPage<ModeratorData>> GetModeratorsAsync(
        string broadcasterId,
        IEnumerable<string>? userIds = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/moderators");
        url.Add("broadcaster_id", broadcasterId);
        url.AddRepeated("user_id", userIds);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseModeratorData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<ModeratorData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Adds a moderator to the broadcaster's chat room.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task AddChannelModeratorAsync(
        string broadcasterId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/moderators");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("user_id", userId);

        await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Removes a moderator from the broadcaster's chat room.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task RemoveChannelModeratorAsync(
        string broadcasterId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/moderators");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("user_id", userId);

        await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    // ── VIPs ─────────────────────────────────────────────────────────

    /// <summary>
    /// Gets a list of the broadcaster's VIPs.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="userIds">The IDs of the users to filter by.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor for forward pagination.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of VIPs.</returns>
    public async Task<TwitchPage<VipData>> GetVipsAsync(
        string broadcasterId,
        IEnumerable<string>? userIds = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channels/vips");
        url.Add("broadcaster_id", broadcasterId);
        url.AddRepeated("user_id", userIds);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseVipData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<VipData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Adds a VIP to the broadcaster's channel.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task AddChannelVipAsync(
        string broadcasterId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channels/vips");
        url.Add("user_id", userId);
        url.Add("broadcaster_id", broadcasterId);

        await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Removes a VIP from the broadcaster's channel.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task RemoveChannelVipAsync(
        string broadcasterId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("channels/vips");
        url.Add("user_id", userId);
        url.Add("broadcaster_id", broadcasterId);

        await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    // ── Shield Mode ──────────────────────────────────────────────────

    /// <summary>
    /// Gets the broadcaster's Shield Mode activation status.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The Shield Mode status data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<ShieldModeData> GetShieldModeStatusAsync(
        string broadcasterId,
        string moderatorId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/shield_mode");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseShieldModeData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Get Shield Mode Status returned no data.");
    }

    /// <summary>
    /// Activates or deactivates the broadcaster's Shield Mode.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="request">The Shield Mode update parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The updated Shield Mode status data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<ShieldModeData> UpdateShieldModeStatusAsync(
        string broadcasterId,
        string moderatorId,
        UpdateShieldModeRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/shield_mode");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.UpdateShieldModeRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Put,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseShieldModeData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Update Shield Mode Status returned no data.");
    }

    // ── Warnings ─────────────────────────────────────────────────────

    /// <summary>
    /// Sends a warning to a user in the broadcaster's chat.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="request">The chat warning parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The warning response data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<WarnResponseData> WarnChatUserAsync(
        string broadcasterId,
        string moderatorId,
        WarnChatUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/warnings");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.WarnChatUserRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseWarnResponseData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Warn Chat User returned no data.");
    }

    // ── Suspicious Users ─────────────────────────────────────────────

    /// <summary>
    /// Adds a suspicious status to a chat user.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="request">The suspicious status parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The suspicious user data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<SuspiciousUserData> AddSuspiciousStatusAsync(
        string broadcasterId,
        string moderatorId,
        AddSuspiciousStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/suspicious_users");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.AddSuspiciousStatusRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseSuspiciousUserData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Add Suspicious Status returned no data.");
    }

    /// <summary>
    /// Removes a suspicious status from a chat user.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The suspicious user data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<SuspiciousUserData> RemoveSuspiciousStatusAsync(
        string broadcasterId,
        string moderatorId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("moderation/suspicious_users");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("user_id", userId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseSuspiciousUserData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Remove Suspicious Status returned no data.");
    }
}
