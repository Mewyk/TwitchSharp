using System.Security.Cryptography;

namespace TwitchSharp.Api.Authentication;

/// <summary>
/// Generates Proof Key for Code Exchange (PKCE) challenges per RFC 7636.
/// Used with the Authorization Code flow to prevent authorization code interception attacks.
/// </summary>
public static class PkceChallenge
{
    /// <summary>
    /// Generates a PKCE code verifier and corresponding S256 code challenge.
    /// </summary>
    /// <returns>A tuple containing the code verifier (43 characters) and the S256 code challenge.</returns>
    public static (string CodeVerifier, string CodeChallenge) Generate()
    {
        // Generate 32 random bytes -> 43-character base64url-encoded code_verifier
        var randomBytes = new byte[32];
        RandomNumberGenerator.Fill(randomBytes);
        var codeVerifier = Base64UrlEncode(randomBytes);

        // code_challenge = BASE64URL(SHA256(ASCII(code_verifier)))
        var challengeBytes = SHA256.HashData(System.Text.Encoding.ASCII.GetBytes(codeVerifier));
        var codeChallenge = Base64UrlEncode(challengeBytes);

        return (codeVerifier, codeChallenge);
    }

    private static string Base64UrlEncode(byte[] input)
    {
        return Convert.ToBase64String(input)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }
}
