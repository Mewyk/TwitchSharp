using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a message to check against AutoMod.
/// </summary>
public sealed record AutoModMessageRequest
{
    /// <summary>A caller-defined ID used to correlate this message with the response.</summary>
    [JsonPropertyName("msg_id")]
    public string MessageId { get; init; } = string.Empty;

    /// <summary>The message to check.</summary>
    [JsonPropertyName("msg_text")]
    public string MessageText { get; init; } = string.Empty;
}
