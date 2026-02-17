namespace TwitchSharp.Api;

/// <summary>
/// Twitch API base URIs.
/// </summary>
internal static class TwitchEndpoints
{
    /// <summary>
    /// Base URI for the Twitch Helix API.
    /// </summary>
    public static readonly Uri HelixBaseUri = new("https://api.twitch.tv/helix/");

    /// <summary>
    /// Base URI for the Twitch OAuth2 endpoints.
    /// </summary>
    public static readonly Uri OAuthBaseUri = new("https://id.twitch.tv/oauth2/");
}
