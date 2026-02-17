using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Whispers API endpoints.
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class WhispersClient
{
    private readonly HelixHttpClient _httpClient;

    internal WhispersClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Sends a whisper message to the specified user.
    /// </summary>
    /// <param name="fromUserId">The ID of the user sending the whisper.</param>
    /// <param name="toUserId">The ID of the user receiving the whisper.</param>
    /// <param name="request">The whisper message parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task SendWhisperAsync(
        string fromUserId,
        string toUserId,
        SendWhisperRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("whispers");
        url.Add("from_user_id", fromUserId);
        url.Add("to_user_id", toUserId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.SendWhisperRequest);

        await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            content,
            cancellationToken).ConfigureAwait(false);
    }
}
