using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private VideosClient? _videos;

    /// <summary>
    /// Gets the Videos API client for managing Twitch videos.
    /// </summary>
    public VideosClient Videos => _videos ??= new VideosClient(_httpClient);
}
