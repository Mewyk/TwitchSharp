using System.Text.Json;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Authentication;

/// <summary>
/// AOT-compatible parser for OIDC ID token JWTs issued by Twitch.
/// Extracts the payload claims without verifying the signature
/// (per OIDC Core section 3.1.3.7, signature validation is optional when
/// the token is received directly from the token endpoint over TLS).
/// </summary>
public static class OidcTokenParser
{
    /// <summary>
    /// Parses the payload of a JWT ID token and returns the deserialized claims.
    /// </summary>
    /// <param name="idToken">The raw JWT string (header.payload.signature).</param>
    /// <returns>The parsed OIDC ID token claims.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="idToken"/> is null.</exception>
    /// <exception cref="FormatException">Thrown when the token is not a valid 3-part JWT or the payload cannot be decoded.</exception>
    public static OidcIdTokenClaims ParseIdToken(string idToken)
    {
        ArgumentNullException.ThrowIfNull(idToken);

        var parts = idToken.Split('.');
        if (parts.Length != 3)
        {
            throw new FormatException("ID token is not a valid JWT (expected 3 dot-separated parts).");
        }

        var payloadBytes = Base64UrlDecode(parts[1]);
        var claims = JsonSerializer.Deserialize(payloadBytes, TwitchApiJsonContext.Default.OidcIdTokenClaims);

        if (claims is null)
        {
            throw new FormatException("Failed to deserialize ID token payload.");
        }

        return claims;
    }

    private static byte[] Base64UrlDecode(string input)
    {
        var base64 = input
            .Replace('-', '+')
            .Replace('_', '/');

        // Add padding if needed
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}
