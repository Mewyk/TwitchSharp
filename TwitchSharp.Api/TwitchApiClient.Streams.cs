using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private StreamsClient? _streams;

    /// <summary>
    /// Gets the Streams API client for managing Twitch stream data.
    /// </summary>
    public StreamsClient Streams => _streams ??= new StreamsClient(_httpClient);
}
