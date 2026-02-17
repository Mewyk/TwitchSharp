using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private ChannelsClient? _channels;

    /// <summary>
    /// Gets the Channels API client for managing Twitch channel data.
    /// </summary>
    public ChannelsClient Channels => _channels ??= new ChannelsClient(_httpClient);
}
