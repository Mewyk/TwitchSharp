using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private HypeTrainClient? _hypeTrain;

    /// <summary>
    /// Gets the Hype Train API client for Hype Train status and events.
    /// </summary>
    public HypeTrainClient HypeTrain => _hypeTrain ??= new HypeTrainClient(_httpClient);
}
