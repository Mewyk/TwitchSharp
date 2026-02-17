using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private PredictionsClient? _predictions;

    /// <summary>
    /// Gets the Predictions API client for managing predictions.
    /// </summary>
    public PredictionsClient Predictions => _predictions ??= new PredictionsClient(_httpClient);
}
