namespace TwitchSharp.Extensions.Authentication;

/// <summary>
/// Configuration options for <see cref="OAuthCallbackListener"/>.
/// </summary>
public sealed class OAuthCallbackListenerOptions
{
    /// <summary>
    /// Custom HTML to serve after a successful OAuth callback.
    /// If <c>null</c>, a default page instructing the user to close the browser window is used.
    /// </summary>
    public string? SuccessHtml { get; set; }

    /// <summary>
    /// Custom HTML to serve when the OAuth callback is missing the required <c>code</c> parameter.
    /// If <c>null</c>, a default error page is used.
    /// </summary>
    public string? ErrorHtml { get; set; }
}
