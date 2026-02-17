using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the response from banning a user or putting them in a timeout.
/// </summary>
public sealed record BanResponseData
{
    /// <summary>The broadcaster whose chat room the user was banned from.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The moderator that banned the user.</summary>
    [JsonPropertyName("moderator_id")]
    public string ModeratorId { get; init; } = string.Empty;

    /// <summary>The user that was banned.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>When the ban or timeout was placed (RFC3339).</summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = string.Empty;

    /// <summary>When the timeout will end (RFC3339), or null if permanently banned.</summary>
    [JsonPropertyName("end_time")]
    public string? EndTime { get; init; }
}
