using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a participant in a shared chat session.
/// </summary>
public sealed record SharedChatParticipant
{
    /// <summary>The participant broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;
}
