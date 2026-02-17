using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private ChatClient? _chat;

    /// <summary>
    /// Gets the Chat API client for managing Twitch chat features.
    /// </summary>
    public ChatClient Chat => _chat ??= new ChatClient(_httpClient);
}
