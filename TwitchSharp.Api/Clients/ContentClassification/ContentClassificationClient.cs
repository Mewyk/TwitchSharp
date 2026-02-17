using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Content Classification Labels API endpoints.
/// </summary>
/// <remarks>All methods may throw <see cref="TwitchApiException"/> on API errors.</remarks>
public sealed class ContentClassificationClient
{
    private readonly HelixHttpClient _httpClient;

    internal ContentClassificationClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets the list of content classification labels.
    /// </summary>
    /// <param name="locale">The locale for the labels. If not specified, labels are returned in English.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The list of content classification label data.</returns>
    public async Task<ContentClassificationLabelData[]> GetContentClassificationLabelsAsync(
        string? locale = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("content_classification_labels");
        url.Add("locale", locale);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.HelixDataResponseContentClassificationLabelData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data ?? [];
    }
}
