using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private ChannelPointsClient? _channelPoints;

    /// <summary>
    /// Gets the Channel Points API client for managing custom rewards and redemptions.
    /// </summary>
    public ChannelPointsClient ChannelPoints => _channelPoints ??= new ChannelPointsClient(_httpClient);
}
