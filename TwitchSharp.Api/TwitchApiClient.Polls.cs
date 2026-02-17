using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private PollsClient? _polls;

    /// <summary>
    /// Gets the Polls API client for managing polls.
    /// </summary>
    public PollsClient Polls => _polls ??= new PollsClient(_httpClient);
}
