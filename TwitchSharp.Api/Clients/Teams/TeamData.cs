using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a team with its members (returned by Get Teams).
/// </summary>
public sealed record TeamData
{
    /// <summary>The list of team members.</summary>
    [JsonPropertyName("users")]
    public TeamMemberData[]? Users { get; init; }

    /// <summary>A URL to the team's background image.</summary>
    [JsonPropertyName("background_image_url")]
    public string? BackgroundImageUrl { get; init; }

    /// <summary>A URL to the team's banner.</summary>
    [JsonPropertyName("banner")]
    public string? Banner { get; init; }

    /// <summary>The UTC date and time (in RFC3339 format) when the team was created.</summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = string.Empty;

    /// <summary>The UTC date and time (in RFC3339 format) of the last team update.</summary>
    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; init; } = string.Empty;

    /// <summary>The team's description.</summary>
    [JsonPropertyName("info")]
    public string Information { get; init; } = string.Empty;

    /// <summary>A URL to the team's logo thumbnail.</summary>
    [JsonPropertyName("thumbnail_url")]
    public string ThumbnailUrl { get; init; } = string.Empty;

    /// <summary>The team's name.</summary>
    [JsonPropertyName("team_name")]
    public string TeamName { get; init; } = string.Empty;

    /// <summary>The team's display name.</summary>
    [JsonPropertyName("team_display_name")]
    public string TeamDisplayName { get; init; } = string.Empty;

    /// <summary>An ID that identifies the team.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;
}
