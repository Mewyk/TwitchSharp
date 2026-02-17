using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for sending an extension chat message.
/// </summary>
public sealed record SendExtensionChatMessageRequest
{
    /// <summary>The message to send (max 280 characters).</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    /// <summary>The ID of the extension sending the message.</summary>
    [JsonPropertyName("extension_id")]
    public string ExtensionId { get; init; } = string.Empty;

    /// <summary>The extension's version number.</summary>
    [JsonPropertyName("extension_version")]
    public string ExtensionVersion { get; init; } = string.Empty;
}
