using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private EntitlementsClient? _entitlements;

    /// <summary>
    /// Gets the Entitlements API client for managing drops entitlements.
    /// </summary>
    public EntitlementsClient Entitlements => _entitlements ??= new EntitlementsClient(_httpClient);
}
