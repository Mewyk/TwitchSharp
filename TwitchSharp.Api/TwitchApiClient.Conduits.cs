using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private ConduitsClient? _conduits;

    /// <summary>
    /// Gets the Conduits API client for managing EventSub conduits and shards.
    /// </summary>
    public ConduitsClient Conduits => _conduits ??= new ConduitsClient(_httpClient);
}
