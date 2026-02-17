using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for setting the extension's required configuration string.
/// </summary>
public sealed record SetExtensionRequiredConfigurationRequest
{
    /// <summary>The ID of the extension to update.</summary>
    [JsonPropertyName("extension_id")]
    public string ExtensionId { get; init; } = string.Empty;

    /// <summary>The extension version number.</summary>
    [JsonPropertyName("extension_version")]
    public string ExtensionVersion { get; init; } = string.Empty;

    /// <summary>The required_configuration string to use.</summary>
    [JsonPropertyName("required_configuration")]
    public string RequiredConfiguration { get; init; } = string.Empty;
}
