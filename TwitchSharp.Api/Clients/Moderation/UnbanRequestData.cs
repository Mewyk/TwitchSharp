using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents an unban request.
/// </summary>
public sealed record UnbanRequestData
{
    /// <summary>The unban request ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The broadcaster's ID.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_name")]
    public string BroadcasterName { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_login")]
    public string BroadcasterLogin { get; init; } = string.Empty;

    /// <summary>The moderator's ID.</summary>
    [JsonPropertyName("moderator_id")]
    public string ModeratorId { get; init; } = string.Empty;

    /// <summary>The moderator's login name.</summary>
    [JsonPropertyName("moderator_login")]
    public string ModeratorLogin { get; init; } = string.Empty;

    /// <summary>The moderator's display name.</summary>
    [JsonPropertyName("moderator_name")]
    public string ModeratorName { get; init; } = string.Empty;

    /// <summary>The requesting user's ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The text of the unban request.</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    /// <summary>The status of the request (pending, approved, denied, acknowledged, canceled).</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>When the unban request was created.</summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = string.Empty;

    /// <summary>When the request was resolved.</summary>
    [JsonPropertyName("resolved_at")]
    public string ResolvedAt { get; init; } = string.Empty;

    /// <summary>The resolution text from the moderator.</summary>
    [JsonPropertyName("resolution_text")]
    public string ResolutionText { get; init; } = string.Empty;
}
