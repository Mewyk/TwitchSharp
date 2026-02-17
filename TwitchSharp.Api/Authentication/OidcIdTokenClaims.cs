using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Authentication;

/// <summary>
/// Parsed claims from an OIDC ID token JWT issued by Twitch.
/// </summary>
public sealed record OidcIdTokenClaims
{
    /// <summary>Issuer: <c>https://id.twitch.tv/oauth2</c>.</summary>
    [JsonPropertyName("iss")]
    public string Iss { get; init; } = string.Empty;

    /// <summary>Subject: the Twitch user ID.</summary>
    [JsonPropertyName("sub")]
    public string Sub { get; init; } = string.Empty;

    /// <summary>Audience: the client ID of the application.</summary>
    [JsonPropertyName("aud")]
    public string Aud { get; init; } = string.Empty;

    /// <summary>Expiration time as a Unix timestamp.</summary>
    [JsonPropertyName("exp")]
    public long Exp { get; init; }

    /// <summary>Issued-at time as a Unix timestamp.</summary>
    [JsonPropertyName("iat")]
    public long Iat { get; init; }

    /// <summary>Anti-replay nonce, if one was included in the authorization request.</summary>
    [JsonPropertyName("nonce")]
    public string? Nonce { get; init; }

    /// <summary>Authorized party (the client ID that was issued the token).</summary>
    [JsonPropertyName("azp")]
    public string? Azp { get; init; }

    /// <summary>The user's email address, if the <c>user:read:email</c> scope was requested.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    /// <summary>Whether the user's email address has been verified.</summary>
    [JsonPropertyName("email_verified")]
    public bool? EmailVerified { get; init; }

    /// <summary>URL of the user's profile image.</summary>
    [JsonPropertyName("picture")]
    public string? Picture { get; init; }

    /// <summary>The user's display name on Twitch.</summary>
    [JsonPropertyName("preferred_username")]
    public string? PreferredUsername { get; init; }

    /// <summary>When the user's profile was last updated.</summary>
    [JsonPropertyName("updated_at")]
    public string? UpdatedAt { get; init; }
}
