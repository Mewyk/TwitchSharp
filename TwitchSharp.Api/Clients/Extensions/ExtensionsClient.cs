using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Extensions API endpoints (OAuth-based only).
/// </summary>
/// <remarks>
/// All methods may throw <see cref="TwitchApiException"/> on API errors.
/// </remarks>
public sealed class ExtensionsClient
{
    private readonly HelixHttpClient _httpClient;

    internal ExtensionsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets information about an extension you own.
    /// Requires an EBS JWT with the <c>external</c> role.
    /// </summary>
    /// <param name="extensionId">The ID of the extension.</param>
    /// <param name="extensionVersion">The version of the extension to get.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The extension data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<ExtensionData> GetExtensionsAsync(
        string extensionId,
        string? extensionVersion = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("extensions");
        url.Add("extension_id", extensionId);
        url.Add("extension_version", extensionVersion);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseExtensionData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Get Extensions returned no data.");
    }

    /// <summary>
    /// Gets information about a released extension.
    /// </summary>
    /// <param name="extensionId">The ID of the extension.</param>
    /// <param name="extensionVersion">The version of the extension to get.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The released extension data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<ExtensionData> GetReleasedExtensionAsync(
        string extensionId,
        string? extensionVersion = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("extensions/released");
        url.Add("extension_id", extensionId);
        url.Add("extension_version", extensionVersion);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseExtensionData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Get Released Extension returned no data.");
    }

    /// <summary>
    /// Gets a list of broadcasters streaming live with the specified extension.
    /// </summary>
    /// <param name="extensionId">The ID of the extension.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of live channels using the extension.</returns>
    public async Task<TwitchPage<ExtensionLiveChannelData>> GetExtensionLiveChannelsAsync(
        string extensionId,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("extensions/live");
        url.Add("extension_id", extensionId);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.ExtensionLiveChannelsResponse,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<ExtensionLiveChannelData>(
            response.Data ?? [],
            response.Pagination);
    }

    /// <summary>
    /// Gets the list of Bits products that belongs to the extension.
    /// </summary>
    /// <param name="shouldIncludeAll">Whether to include disabled or expired products.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of extension Bits products.</returns>
    public async Task<ExtensionBitsProductData[]> GetExtensionBitsProductsAsync(
        bool? shouldIncludeAll = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("bits/extensions");
        url.Add("should_include_all", shouldIncludeAll);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseExtensionBitsProductData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Adds or updates a Bits product for the extension.
    /// </summary>
    /// <param name="request">The Bits product data to add or update.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of updated extension Bits products.</returns>
    public async Task<ExtensionBitsProductData[]> UpdateExtensionBitsProductAsync(
        UpdateExtensionBitsProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.UpdateExtensionBitsProductRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Put,
            "bits/extensions",
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseExtensionBitsProductData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Gets the specified configuration segments for the extension.
    /// </summary>
    /// <param name="extensionId">The ID of the extension.</param>
    /// <param name="segments">The configuration segment types to get (broadcaster, developer, global).</param>
    /// <param name="broadcasterId">The broadcaster ID. Required if requesting broadcaster or developer segments.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The configuration segments.</returns>
    public async Task<ExtensionConfigurationData[]> GetExtensionConfigurationAsync(
        string extensionId,
        string[] segments,
        string? broadcasterId = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("extensions/configurations");
        url.Add("extension_id", extensionId);
        url.AddRepeated("segment", segments);
        url.Add("broadcaster_id", broadcasterId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseExtensionConfigurationData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Sets the specified configuration segment for the extension.
    /// </summary>
    /// <param name="request">The configuration segment data to set.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task SetExtensionConfigurationAsync(
        SetExtensionConfigurationRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.SetExtensionConfigurationRequest);

        await _httpClient.SendAsync(
            HttpMethod.Put,
            "extensions/configurations",
            TwitchAuthenticationMode.AppToken,
            content,
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Sets the extension's required configuration string for the specified broadcaster.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster who installed the extension.</param>
    /// <param name="request">The required configuration data.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task SetExtensionRequiredConfigurationAsync(
        string broadcasterId,
        SetExtensionRequiredConfigurationRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("extensions/required_configuration");
        url.Add("broadcaster_id", broadcasterId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.SetExtensionRequiredConfigurationRequest);

        await _httpClient.SendAsync(
            HttpMethod.Put,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            content,
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a PubSub message to the specified extension on a channel or globally.
    /// </summary>
    /// <param name="request">The PubSub message data.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task SendExtensionPubSubMessageAsync(
        SendExtensionPubSubMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.SendExtensionPubSubMessageRequest);

        await _httpClient.SendAsync(
            HttpMethod.Post,
            "extensions/pubsub",
            TwitchAuthenticationMode.AppToken,
            content,
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a chat message to the specified channel as the extension.
    /// The extension must have Chat Capabilities enabled.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster whose chat to send the message to.</param>
    /// <param name="request">The chat message data.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task SendExtensionChatMessageAsync(
        string broadcasterId,
        SendExtensionChatMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("extensions/chat");
        url.Add("broadcaster_id", broadcasterId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.SendExtensionChatMessageRequest);

        await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            content,
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the extension's JWT secrets.
    /// </summary>
    /// <param name="extensionId">The ID of the extension.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The extension secret data.</returns>
    public async Task<ExtensionSecretData[]> GetExtensionSecretsAsync(
        string extensionId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("extensions/jwt/secrets");
        url.Add("extension_id", extensionId);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseExtensionSecretData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }

    /// <summary>
    /// Creates a new JWT secret for the extension. Creating a new secret removes
    /// the current secrets from the service, so only use this when ready to switch.
    /// </summary>
    /// <param name="extensionId">The ID of the extension.</param>
    /// <param name="delay">Seconds to delay activating the new secret (minimum 300).</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The extension secret data including old and new secrets.</returns>
    public async Task<ExtensionSecretData[]> CreateExtensionSecretAsync(
        string extensionId,
        int? delay = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("extensions/jwt/secrets");
        url.Add("extension_id", extensionId);
        url.Add("delay", delay);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseExtensionSecretData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }
}
