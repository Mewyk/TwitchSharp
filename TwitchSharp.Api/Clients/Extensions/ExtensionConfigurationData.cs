using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents an extension configuration segment.
/// </summary>
public sealed record ExtensionConfigurationData
{
    /// <summary>The configuration segment type (broadcaster, developer, or global).</summary>
    [JsonPropertyName("segment")]
    public string Segment { get; init; } = string.Empty;

    /// <summary>The segment contents (plain text or JSON string).</summary>
    [JsonPropertyName("content")]
    public string Content { get; init; } = string.Empty;

    /// <summary>Version number identifying this configuration definition.</summary>
    [JsonPropertyName("version")]
    public string Version { get; init; } = string.Empty;
}
