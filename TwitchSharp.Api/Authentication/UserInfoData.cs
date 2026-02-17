using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Authentication;

/// <summary>
/// Response from the Twitch OIDC UserInfo endpoint (<c>https://id.twitch.tv/oauth2/userinfo</c>).
/// </summary>
public sealed record UserInfoData
{
    /// <summary>The Twitch user ID.</summary>
    [JsonPropertyName("sub")]
    public string Sub { get; init; } = string.Empty;

    /// <summary>The user's display name on Twitch.</summary>
    [JsonPropertyName("preferred_username")]
    public string? PreferredUsername { get; init; }

    /// <summary>URL of the user's profile image.</summary>
    [JsonPropertyName("picture")]
    public string? Picture { get; init; }

    /// <summary>The user's email address, if available.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    /// <summary>Whether the user's email address has been verified.</summary>
    [JsonPropertyName("email_verified")]
    public bool? EmailVerified { get; init; }

    /// <summary>When the user's profile was last updated.</summary>
    [JsonPropertyName("updated_at")]
    public string? UpdatedAt { get; init; }
}
