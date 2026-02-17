using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Chat API endpoints.
/// </summary>
/// <remarks>All methods may throw <see cref="TwitchApiException"/> on API errors.</remarks>
public sealed class ChatClient
{
    private readonly HelixHttpClient _httpClient;

    internal ChatClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets a page of users in the specified broadcaster's chat.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of chatters.</returns>
    public async Task<TwitchPage<ChatterData>> GetChattersAsync(
        string broadcasterId,
        string moderatorId,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("chat/chatters");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseChatterData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<ChatterData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Gets the channel emotes for the specified broadcaster.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of channel emotes.</returns>
    public async Task<IReadOnlyList<EmoteData>> GetChannelEmotesAsync(
        string broadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("chat/emotes");
        url.Add("broadcaster_id", broadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseEmoteData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Gets the global emotes available on Twitch.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of global emotes.</returns>
    public async Task<IReadOnlyList<EmoteData>> GetGlobalEmotesAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            "chat/emotes/global",
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseEmoteData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Gets emotes from one or more emote sets.
    /// </summary>
    /// <param name="emoteSetIds">The emote set IDs to get emotes for.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of emotes.</returns>
    public async Task<IReadOnlyList<EmoteData>> GetEmoteSetsAsync(
        IEnumerable<string> emoteSetIds,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("chat/emotes/set");
        url.AddRepeated("emote_set_id", emoteSetIds);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseEmoteData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Gets a page of emotes the specified user can use in chat.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of user emotes.</returns>
    public async Task<TwitchPage<EmoteData>> GetUserEmotesAsync(
        string userId,
        string? broadcasterId = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("chat/emotes/user");
        url.Add("user_id", userId);
        url.Add("broadcaster_id", broadcasterId);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseEmoteData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<EmoteData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Gets the chat badges for the specified broadcaster's channel.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of channel chat badges.</returns>
    public async Task<IReadOnlyList<BadgeSetData>> GetChannelChatBadgesAsync(
        string broadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("chat/badges");
        url.Add("broadcaster_id", broadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseBadgeSetData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Gets the global chat badges available on Twitch.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of global chat badges.</returns>
    public async Task<IReadOnlyList<BadgeSetData>> GetGlobalChatBadgesAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            "chat/badges/global",
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseBadgeSetData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Gets the chat settings for the specified broadcaster's channel.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The chat settings.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<ChatSettingsData> GetChatSettingsAsync(
        string broadcasterId,
        string? moderatorId = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("chat/settings");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseChatSettingsData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Get Chat Settings returned no data.");
    }

    /// <summary>
    /// Updates the chat settings for the specified broadcaster's channel.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="request">The update chat settings parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The updated chat settings.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<ChatSettingsData> UpdateChatSettingsAsync(
        string broadcasterId,
        string moderatorId,
        UpdateChatSettingsRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("chat/settings");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.UpdateChatSettingsRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Patch,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseChatSettingsData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Update Chat Settings returned no data.");
    }

    /// <summary>
    /// Gets the chat color for one or more users.
    /// </summary>
    /// <param name="userIds">The user IDs to get chat colors for.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of user chat colors.</returns>
    public async Task<IReadOnlyList<ChatColorData>> GetUserChatColorAsync(
        IEnumerable<string> userIds,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("chat/color");
        url.AddRepeated("user_id", userIds);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseChatColorData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Updates the chat color for the specified user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="color">The color to use for the user's name in chat.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task UpdateUserChatColorAsync(
        string userId,
        string color,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("chat/color");
        url.Add("user_id", userId);
        url.Add("color", color);

        await _httpClient.SendAsync(
            HttpMethod.Put,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Sends an announcement to the specified broadcaster's chat.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="request">The send announcement parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task SendChatAnnouncementAsync(
        string broadcasterId,
        string moderatorId,
        SendAnnouncementRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("chat/announcements");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("moderator_id", moderatorId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.SendAnnouncementRequest);

        await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            content,
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a shoutout to the specified broadcaster.
    /// </summary>
    /// <param name="fromBroadcasterId">The ID of the broadcaster sending the shoutout.</param>
    /// <param name="toBroadcasterId">The ID of the broadcaster receiving the shoutout.</param>
    /// <param name="moderatorId">The ID of the moderator.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task SendShoutoutAsync(
        string fromBroadcasterId,
        string toBroadcasterId,
        string moderatorId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("chat/shoutouts");
        url.Add("from_broadcaster_id", fromBroadcasterId);
        url.Add("to_broadcaster_id", toBroadcasterId);
        url.Add("moderator_id", moderatorId);

        await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a chat message to the specified broadcaster's chat.
    /// </summary>
    /// <param name="request">The send message parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The send message response.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<SendMessageResponseData> SendChatMessageAsync(
        SendMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.SendMessageRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            "chat/messages",
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponseSendMessageResponseData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Send Chat Message returned no data.");
    }

    /// <summary>
    /// Gets the active shared chat session for the specified broadcaster.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The shared chat session.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<SharedChatSessionData> GetSharedChatSessionAsync(
        string broadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("shared_chat/session");
        url.Add("broadcaster_id", broadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseSharedChatSessionData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Get Shared Chat Session returned no data.");
    }
}
