using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a blocked term.
/// </summary>
public sealed record BlockedTermData
{
    /// <summary>The broadcaster that owns the list of blocked terms.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The moderator that blocked the word or phrase.</summary>
    [JsonPropertyName("moderator_id")]
    public string ModeratorId { get; init; } = string.Empty;

    /// <summary>An ID that identifies this blocked term.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The blocked word or phrase.</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    /// <summary>When the term was blocked (RFC3339).</summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = string.Empty;

    /// <summary>When the term was updated (RFC3339).</summary>
    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; init; } = string.Empty;

    /// <summary>When the blocked term expires (RFC3339), or null if permanent.</summary>
    [JsonPropertyName("expires_at")]
    public string? ExpiresAt { get; init; }
}
