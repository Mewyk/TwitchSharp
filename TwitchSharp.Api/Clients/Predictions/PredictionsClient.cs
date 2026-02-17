using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Predictions API endpoints.
/// </summary>
/// <remarks>All methods may throw <see cref="TwitchApiException"/> on API errors.</remarks>
public sealed class PredictionsClient
{
    private readonly HelixHttpClient _httpClient;

    internal PredictionsClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets a page of predictions for the specified broadcaster.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="ids">The IDs of the predictions to get. If not specified, all predictions are returned.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of predictions.</returns>
    public async Task<TwitchPage<PredictionData>> GetPredictionsAsync(
        string broadcasterId,
        IEnumerable<string>? ids = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("predictions");
        url.Add("broadcaster_id", broadcasterId);
        url.AddRepeated("id", ids);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponsePredictionData,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<PredictionData>(
            response.Data ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Creates a prediction. The prediction begins immediately.
    /// </summary>
    /// <param name="request">The create prediction parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The created prediction.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<PredictionData> CreatePredictionAsync(
        CreatePredictionRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.CreatePredictionRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            "predictions",
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponsePredictionData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("Create Prediction returned no data.");
    }

    /// <summary>
    /// Ends a prediction that is currently active or locked.
    /// </summary>
    /// <param name="request">The end prediction parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The ended prediction.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns no data.</exception>
    public async Task<PredictionData> EndPredictionAsync(
        EndPredictionRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.EndPredictionRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Patch,
            "predictions",
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.HelixDataResponsePredictionData,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data is { Length: > 0 }
            ? response.Data[0]
            : throw new InvalidOperationException("End Prediction returned no data.");
    }
}
