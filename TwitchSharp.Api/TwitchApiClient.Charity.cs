using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private CharityClient? _charity;

    /// <summary>
    /// Gets the Charity API client for retrieving charity campaign information.
    /// </summary>
    public CharityClient Charity => _charity ??= new CharityClient(_httpClient);
}
