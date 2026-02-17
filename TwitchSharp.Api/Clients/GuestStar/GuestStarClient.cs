using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Guest Star API endpoints.
/// All Guest Star endpoints are in BETA and may change without notice.
/// </summary>
/// <remarks>All methods may throw <see cref="TwitchApiException"/> on API errors.</remarks>
public sealed class GuestStarClient
{
    private readonly HelixHttpClient _httpClient;

    internal GuestStarClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets the Guest Star settings for a channel.
    /// BETA: This endpoint is in beta and may change without notice.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a moderator.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The channel's Guest Star settings.</returns>
    /// <exception cref="TwitchApiException">Thrown when the API returns no settings data.</exception>
    public async Task<GuestStarSettingsData> GetChannelGuestStarSettingsAsync(
        string broadcasterId,
        string moderatorId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("guest_star/channel_settings");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseGuestStarSettingsData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new TwitchApiException(
                TwitchErrorCodes.Unexpected,
                "Get Channel Guest Star Settings returned no data.",
                endpoint: "guest_star/channel_settings");
    }

    /// <summary>
    /// Updates the Guest Star settings for a channel.
    /// BETA: This endpoint is in beta and may change without notice.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="isModeratorSendLiveEnabled">Whether moderators can send guests live.</param>
    /// <param name="slotCount">Number of Guest Star slots (1-6).</param>
    /// <param name="isBrowserSourceAudioEnabled">Whether browser source audio is enabled.</param>
    /// <param name="groupLayout">The group layout type.</param>
    /// <param name="regenerateBrowserSources">Whether to regenerate browser source tokens.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task UpdateChannelGuestStarSettingsAsync(
        string broadcasterId,
        bool? isModeratorSendLiveEnabled = null,
        int? slotCount = null,
        bool? isBrowserSourceAudioEnabled = null,
        string? groupLayout = null,
        bool? regenerateBrowserSources = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("guest_star/channel_settings");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("is_moderator_send_live_enabled", isModeratorSendLiveEnabled);
        url.Add("slot_count", slotCount);
        url.Add("is_browser_source_audio_enabled", isBrowserSourceAudioEnabled);
        url.Add("group_layout", groupLayout);
        url.Add("regenerate_browser_sources", regenerateBrowserSources);

        await _httpClient.SendAsync(
            HttpMethod.Put,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the active Guest Star session for a channel.
    /// BETA: This endpoint is in beta and may change without notice.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster hosting the session.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a moderator.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The active Guest Star session.</returns>
    /// <exception cref="TwitchApiException">Thrown when the API returns no session data.</exception>
    public async Task<GuestStarSessionData> GetGuestStarSessionAsync(
        string broadcasterId,
        string moderatorId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("guest_star/session");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseGuestStarSessionData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new TwitchApiException(
                TwitchErrorCodes.Unexpected,
                "Get Guest Star Session returned no data.",
                endpoint: "guest_star/session");
    }

    /// <summary>
    /// Creates a new Guest Star session for the broadcaster.
    /// BETA: This endpoint is in beta and may change without notice.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The created Guest Star session.</returns>
    /// <exception cref="TwitchApiException">Thrown when the API returns no session data.</exception>
    public async Task<GuestStarSessionData> CreateGuestStarSessionAsync(
        string broadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("guest_star/session");
        url.Add("broadcaster_id", broadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseGuestStarSessionData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new TwitchApiException(
                TwitchErrorCodes.Unexpected,
                "Create Guest Star Session returned no data.",
                endpoint: "guest_star/session");
    }

    /// <summary>
    /// Ends a Guest Star session.
    /// BETA: This endpoint is in beta and may change without notice.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="sessionId">The session ID to end.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The ended session data.</returns>
    /// <exception cref="TwitchApiException">Thrown when the API returns no session data.</exception>
    public async Task<GuestStarSessionData> EndGuestStarSessionAsync(
        string broadcasterId,
        string sessionId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("guest_star/session");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("session_id", sessionId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseGuestStarSessionData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new TwitchApiException(
                TwitchErrorCodes.Unexpected,
                "End Guest Star Session returned no data.",
                endpoint: "guest_star/session");
    }

    /// <summary>
    /// Gets the Guest Star invitations for a session.
    /// BETA: This endpoint is in beta and may change without notice.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a moderator.</param>
    /// <param name="sessionId">The session ID.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of invitations.</returns>
    public async Task<GuestStarInviteData[]> GetGuestStarInvitesAsync(
        string broadcasterId,
        string moderatorId,
        string sessionId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("guest_star/invites");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("session_id", sessionId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseGuestStarInviteData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Sends a Guest Star invite to a user.
    /// BETA: This endpoint is in beta and may change without notice.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a moderator.</param>
    /// <param name="sessionId">The session ID.</param>
    /// <param name="guestId">The user ID to invite.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task SendGuestStarInviteAsync(
        string broadcasterId,
        string moderatorId,
        string sessionId,
        string guestId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("guest_star/invites");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("session_id", sessionId);
        url.Add("guest_id", guestId);

        await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a Guest Star invite.
    /// BETA: This endpoint is in beta and may change without notice.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a moderator.</param>
    /// <param name="sessionId">The session ID.</param>
    /// <param name="guestId">The user ID whose invite to delete.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task DeleteGuestStarInviteAsync(
        string broadcasterId,
        string moderatorId,
        string sessionId,
        string guestId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("guest_star/invites");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("session_id", sessionId);
        url.Add("guest_id", guestId);

        await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Assigns a guest to a slot in a Guest Star session.
    /// BETA: This endpoint is in beta and may change without notice.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a moderator.</param>
    /// <param name="sessionId">The session ID.</param>
    /// <param name="guestId">The user ID to assign.</param>
    /// <param name="slotId">The slot identifier (1 through N).</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task AssignGuestStarSlotAsync(
        string broadcasterId,
        string moderatorId,
        string sessionId,
        string guestId,
        string slotId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("guest_star/slot");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("session_id", sessionId);
        url.Add("guest_id", guestId);
        url.Add("slot_id", slotId);

        await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Moves a guest from one slot to another in a Guest Star session.
    /// BETA: This endpoint is in beta and may change without notice.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a moderator.</param>
    /// <param name="sessionId">The session ID.</param>
    /// <param name="sourceSlotId">The slot to move from.</param>
    /// <param name="destinationSlotId">The slot to move to.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task UpdateGuestStarSlotAsync(
        string broadcasterId,
        string moderatorId,
        string sessionId,
        string sourceSlotId,
        string? destinationSlotId = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("guest_star/slot");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("session_id", sessionId);
        url.Add("source_slot_id", sourceSlotId);
        url.Add("destination_slot_id", destinationSlotId);

        await _httpClient.SendAsync(
            HttpMethod.Patch,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Removes a guest from a slot in a Guest Star session.
    /// BETA: This endpoint is in beta and may change without notice.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a moderator.</param>
    /// <param name="sessionId">The session ID.</param>
    /// <param name="guestId">The user ID of the guest to remove.</param>
    /// <param name="slotId">The slot to clear.</param>
    /// <param name="shouldReinviteGuest">Whether the guest should be sent a new invite after removal.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task DeleteGuestStarSlotAsync(
        string broadcasterId,
        string moderatorId,
        string sessionId,
        string guestId,
        string slotId,
        bool? shouldReinviteGuest = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("guest_star/slot");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("session_id", sessionId);
        url.Add("guest_id", guestId);
        url.Add("slot_id", slotId);
        url.Add("should_reinvite_guest", shouldReinviteGuest);

        await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the audio/video settings for a Guest Star slot.
    /// BETA: This endpoint is in beta and may change without notice.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a moderator.</param>
    /// <param name="sessionId">The session ID.</param>
    /// <param name="slotId">The slot to update.</param>
    /// <param name="isAudioEnabled">Whether audio is enabled for the slot.</param>
    /// <param name="isVideoEnabled">Whether video is enabled for the slot.</param>
    /// <param name="isLive">Whether the slot is live.</param>
    /// <param name="volume">The slot volume (0-100).</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task UpdateGuestStarSlotSettingsAsync(
        string broadcasterId,
        string moderatorId,
        string sessionId,
        string slotId,
        bool? isAudioEnabled = null,
        bool? isVideoEnabled = null,
        bool? isLive = null,
        int? volume = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("guest_star/slot_settings");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("session_id", sessionId);
        url.Add("slot_id", slotId);
        url.Add("is_audio_enabled", isAudioEnabled);
        url.Add("is_video_enabled", isVideoEnabled);
        url.Add("is_live", isLive);
        url.Add("volume", volume);

        await _httpClient.SendAsync(
            HttpMethod.Patch,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
