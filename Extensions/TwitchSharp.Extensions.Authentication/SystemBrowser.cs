using System.Diagnostics;

namespace TwitchSharp.Extensions.Authentication;

/// <summary>
/// Provides methods to open URLs in the user's default system browser.
/// Uses <see cref="Process.Start(ProcessStartInfo)"/> with <c>UseShellExecute = true</c>,
/// which is the standard cross-platform pattern used by Microsoft's MSAL library.
/// </summary>
public static class SystemBrowser
{
    /// <summary>
    /// Opens the specified URI in the system's default browser.
    /// </summary>
    /// <param name="uri">The URI to open.</param>
    public static void Open(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        Open(uri.AbsoluteUri);
    }

    /// <summary>
    /// Opens the specified URL in the system's default browser.
    /// </summary>
    /// <param name="url">The URL to open.</param>
    public static void Open(string url)
    {
        ArgumentNullException.ThrowIfNull(url);

        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }
}
