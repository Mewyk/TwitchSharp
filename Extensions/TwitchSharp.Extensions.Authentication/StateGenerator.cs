using System.Security.Cryptography;

namespace TwitchSharp.Extensions.Authentication;

/// <summary>
/// Generates cryptographically random state strings for OAuth CSRF protection.
/// The generated strings are URL-safe and suitable for use as the <c>state</c> parameter
/// in OAuth authorization requests.
/// </summary>
public static class StateGenerator
{
    /// <summary>
    /// Generates a cryptographically random URL-safe state string.
    /// </summary>
    /// <returns>A 43-character base64url-encoded random string.</returns>
    public static string Generate()
    {
        var randomBytes = new byte[32];
        RandomNumberGenerator.Fill(randomBytes);
        return Base64UrlEncode(randomBytes);
    }

    private static string Base64UrlEncode(byte[] input)
    {
        return Convert.ToBase64String(input)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }
}
