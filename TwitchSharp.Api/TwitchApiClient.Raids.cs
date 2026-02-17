using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private RaidsClient? _raids;

    /// <summary>
    /// Gets the Raids API client for starting and canceling raids.
    /// </summary>
    public RaidsClient Raids => _raids ??= new RaidsClient(_httpClient);
}
