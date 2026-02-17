using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a Twitch user returned by the Get Users and Update User endpoints.
/// </summary>
public sealed record UserData
{
    /// <summary>The user's ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The user's login name.</summary>
    [JsonPropertyName("login")]
    public string Login { get; init; } = string.Empty;

    /// <summary>The user's display name.</summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; init; } = string.Empty;

    /// <summary>The user's type: "", "admin", "global_mod", or "staff".</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The user's broadcaster type: "", "affiliate", or "partner".</summary>
    [JsonPropertyName("broadcaster_type")]
    public string BroadcasterType { get; init; } = string.Empty;

    /// <summary>The user's channel description.</summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    /// <summary>URL of the user's profile image.</summary>
    [JsonPropertyName("profile_image_url")]
    public string ProfileImageUrl { get; init; } = string.Empty;

    /// <summary>URL of the user's offline image.</summary>
    [JsonPropertyName("offline_image_url")]
    public string OfflineImageUrl { get; init; } = string.Empty;

    /// <summary>The user's email address. Only included when the user:read:email scope is used.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    /// <summary>The date and time the user's account was created.</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }
}
