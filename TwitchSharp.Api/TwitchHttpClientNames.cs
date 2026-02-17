namespace TwitchSharp.Api;

/// <summary>
/// Named HttpClient identifiers for use with <c>IHttpClientFactory</c>.
/// </summary>
public static class TwitchHttpClientNames
{
    /// <summary>
    /// The named HttpClient for Twitch Helix API requests.
    /// </summary>
    public const string Helix = "TwitchSharp.Helix";

    /// <summary>
    /// The named HttpClient for Twitch OAuth2 token requests.
    /// </summary>
    public const string OAuth = "TwitchSharp.OAuth";
}
