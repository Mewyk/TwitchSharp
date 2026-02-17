using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private ExtensionsClient? _extensions;

    /// <summary>
    /// Gets the Extensions API client for managing Twitch extensions.
    /// </summary>
    public ExtensionsClient Extensions => _extensions ??= new ExtensionsClient(_httpClient);
}
