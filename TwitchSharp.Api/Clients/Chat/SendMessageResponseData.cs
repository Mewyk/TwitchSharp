using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the response from the Send Chat Message endpoint.
/// </summary>
public sealed record SendMessageResponseData
{
    /// <summary>The ID of the sent message.</summary>
    [JsonPropertyName("message_id")]
    public string MessageId { get; init; } = string.Empty;

    /// <summary>Whether the message was successfully sent.</summary>
    [JsonPropertyName("is_sent")]
    public bool IsSent { get; init; }

    /// <summary>The reason the message was dropped, if applicable.</summary>
    [JsonPropertyName("drop_reason")]
    public MessageDropReason? DropReason { get; init; }
}
