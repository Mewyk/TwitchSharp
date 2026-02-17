namespace TwitchSharp;

/// <summary>
/// Utility methods for redacting secrets from log output and exception messages.
/// </summary>
public static class LogRedaction
{
    private const string RedactedPlaceholder = "***";
    private const int VisiblePrefixLength = 4;

    /// <summary>
    /// Redacts a secret value, showing only the first few characters.
    /// Returns <see cref="RedactedPlaceholder"/> for <c>null</c> or short values.
    /// </summary>
    public static string RedactSecret(string? value)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= VisiblePrefixLength)
        {
            return RedactedPlaceholder;
        }

        return string.Concat(value.AsSpan(0, VisiblePrefixLength), RedactedPlaceholder);
    }

    /// <summary>
    /// Fully redacts a value, replacing it entirely with a placeholder.
    /// </summary>
    public static string RedactFull(string? value) =>
        string.IsNullOrEmpty(value) ? RedactedPlaceholder : RedactedPlaceholder;

    /// <summary>
    /// Extracts the path portion of a URL, stripping query string and fragment.
    /// Returns the path-only string safe for logging.
    /// </summary>
    public static string GetSafePath(string? url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return string.Empty;
        }

        var queryIndex = url.IndexOf('?');
        if (queryIndex >= 0)
        {
            return url[..queryIndex];
        }

        var fragmentIndex = url.IndexOf('#');
        if (fragmentIndex >= 0)
        {
            return url[..fragmentIndex];
        }

        return url;
    }
}
