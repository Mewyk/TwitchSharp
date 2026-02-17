using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a banned user.
/// </summary>
public sealed record BannedUserData
{
    /// <summary>The ID of the banned user.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The banned user's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The banned user's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>When the timeout expires (RFC3339), or empty string if permanently banned.</summary>
    [JsonPropertyName("expires_at")]
    public string ExpiresAt { get; init; } = string.Empty;

    /// <summary>When the user was banned (RFC3339).</summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = string.Empty;

    /// <summary>The reason the user was banned, if provided.</summary>
    [JsonPropertyName("reason")]
    public string Reason { get; init; } = string.Empty;

    /// <summary>The ID of the moderator that banned the user.</summary>
    [JsonPropertyName("moderator_id")]
    public string ModeratorId { get; init; } = string.Empty;

    /// <summary>The moderator's login name.</summary>
    [JsonPropertyName("moderator_login")]
    public string ModeratorLogin { get; init; } = string.Empty;

    /// <summary>The moderator's display name.</summary>
    [JsonPropertyName("moderator_name")]
    public string ModeratorName { get; init; } = string.Empty;
}
