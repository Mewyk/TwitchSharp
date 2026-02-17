using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private AdsClient? _ads;

    /// <summary>
    /// Gets the Ads API client for managing commercials and ad schedules.
    /// </summary>
    public AdsClient Ads => _ads ??= new AdsClient(_httpClient);
}
