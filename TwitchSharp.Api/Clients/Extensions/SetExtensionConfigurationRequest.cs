using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for setting an extension configuration segment.
/// </summary>
public sealed record SetExtensionConfigurationRequest
{
    /// <summary>The ID of the extension to update.</summary>
    [JsonPropertyName("extension_id")]
    public string ExtensionId { get; init; } = string.Empty;

    /// <summary>The configuration segment type (broadcaster, developer, or global).</summary>
    [JsonPropertyName("segment")]
    public string Segment { get; init; } = string.Empty;

    /// <summary>The broadcaster ID. Required if segment is broadcaster or developer.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string? BroadcasterId { get; init; }

    /// <summary>The segment contents (plain text or JSON string, max 5 KB).</summary>
    [JsonPropertyName("content")]
    public string? Content { get; init; }

    /// <summary>Version number to update.</summary>
    [JsonPropertyName("version")]
    public string? Version { get; init; }
}
