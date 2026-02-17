using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private WhispersClient? _whispers;

    /// <summary>
    /// Gets the Whispers API client for sending whisper messages.
    /// </summary>
    public WhispersClient Whispers => _whispers ??= new WhispersClient(_httpClient);
}
