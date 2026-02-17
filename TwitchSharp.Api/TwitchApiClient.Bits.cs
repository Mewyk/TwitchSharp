using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private BitsClient? _bits;

    /// <summary>
    /// Gets the Bits API client for Bits leaderboard and Cheermotes.
    /// </summary>
    public BitsClient Bits => _bits ??= new BitsClient(_httpClient);
}
