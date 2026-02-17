using System.Text;
using System.Text.Json;
using TwitchSharp.Api.Authentication;

namespace ApiTests;

public sealed class OidcTokenParserTests
{
    [Fact]
    public void ParseIdToken_ValidJwt_ReturnsParsedClaims()
    {
        var claims = new
        {
            iss = "https://id.twitch.tv/oauth2",
            sub = "12345",
            aud = "client_id_here",
            exp = 1700000000L,
            iat = 1699999000L,
            preferred_username = "testuser"
        };

        var payloadJson = JsonSerializer.Serialize(claims);
        var header = Base64UrlEncode("{\"alg\":\"RS256\",\"typ\":\"JWT\"}"u8.ToArray());
        var payload = Base64UrlEncode(Encoding.UTF8.GetBytes(payloadJson));
        var signature = Base64UrlEncode("fake_signature"u8.ToArray());
        var jwt = $"{header}.{payload}.{signature}";

        var result = OidcTokenParser.ParseIdToken(jwt);

        Assert.Equal("https://id.twitch.tv/oauth2", result.Iss);
        Assert.Equal("12345", result.Sub);
        Assert.Equal("client_id_here", result.Aud);
        Assert.Equal(1700000000L, result.Exp);
        Assert.Equal(1699999000L, result.Iat);
        Assert.Equal("testuser", result.PreferredUsername);
    }

    [Fact]
    public void ParseIdToken_NullInput_ThrowsArgumentNullException()
    {
#nullable disable
        string nullToken = null;
        Assert.Throws<ArgumentNullException>(() => OidcTokenParser.ParseIdToken(nullToken));
#nullable restore
    }

    [Fact]
    public void ParseIdToken_TwoParts_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => OidcTokenParser.ParseIdToken("header.payload"));
    }

    [Fact]
    public void ParseIdToken_OnePart_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => OidcTokenParser.ParseIdToken("singlepart"));
    }

    [Fact]
    public void ParseIdToken_FourParts_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => OidcTokenParser.ParseIdToken("a.b.c.d"));
    }

    [Fact]
    public void ParseIdToken_ValidJwtWithOptionalClaims_ParsesNullableFields()
    {
        var claims = new
        {
            iss = "https://id.twitch.tv/oauth2",
            sub = "67890",
            aud = "another_client",
            exp = 1700000000L,
            iat = 1699999000L,
            nonce = "test_nonce",
            email = "user@example.com",
            email_verified = true
        };

        var payloadJson = JsonSerializer.Serialize(claims);
        var header = Base64UrlEncode("{\"alg\":\"RS256\"}"u8.ToArray());
        var payload = Base64UrlEncode(Encoding.UTF8.GetBytes(payloadJson));
        var signature = Base64UrlEncode("sig"u8.ToArray());
        var jwt = $"{header}.{payload}.{signature}";

        var result = OidcTokenParser.ParseIdToken(jwt);

        Assert.Equal("test_nonce", result.Nonce);
        Assert.Equal("user@example.com", result.Email);
        Assert.True(result.EmailVerified);
    }

    private static string Base64UrlEncode(byte[] input)
    {
        return Convert.ToBase64String(input)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }
}
