using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a shared chat session returned by the Get Shared Chat Session endpoint.
/// </summary>
public sealed record SharedChatSessionData
{
    /// <summary>The shared chat session's ID.</summary>
    [JsonPropertyName("session_id")]
    public string SessionId { get; init; } = string.Empty;

    /// <summary>The host broadcaster's user ID.</summary>
    [JsonPropertyName("host_broadcaster_id")]
    public string HostBroadcasterId { get; init; } = string.Empty;

    /// <summary>The participants in the shared chat session.</summary>
    [JsonPropertyName("participants")]
    public SharedChatParticipant[] Participants { get; init; } = [];

    /// <summary>The date and time the session was created.</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>The date and time the session was last updated.</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; init; }
}
