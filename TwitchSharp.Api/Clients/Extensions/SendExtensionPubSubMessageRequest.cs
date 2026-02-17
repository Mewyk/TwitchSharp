using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for sending an extension PubSub message.
/// </summary>
public sealed record SendExtensionPubSubMessageRequest
{
    /// <summary>The message targets (e.g., "broadcast", "global", "whisper-{userId}").</summary>
    [JsonPropertyName("target")]
    public string[] Target { get; init; } = [];

    /// <summary>The broadcaster ID to receive the message. Omit if <see cref="IsGlobalBroadcast"/> is true.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string? BroadcasterId { get; init; }

    /// <summary>Whether to send to all channels where the extension is active.</summary>
    [JsonPropertyName("is_global_broadcast")]
    public bool? IsGlobalBroadcast { get; init; }

    /// <summary>The message to send (plain text or JSON string, max 5 KB).</summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;
}
