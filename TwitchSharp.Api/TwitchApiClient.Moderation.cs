using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private ModerationClient? _moderation;

    /// <summary>
    /// Gets the Moderation API client for managing bans, AutoMod, blocked terms, moderators, VIPs, and more.
    /// </summary>
    public ModerationClient Moderation => _moderation ??= new ModerationClient(_httpClient);
}
