using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents extension JWT secret data containing a format version and list of secrets.
/// </summary>
public sealed record ExtensionSecretData
{
    /// <summary>Version number identifying the secret's data format.</summary>
    [JsonPropertyName("format_version")]
    public int FormatVersion { get; init; }

    /// <summary>The list of secrets.</summary>
    [JsonPropertyName("secrets")]
    public ExtensionSecretEntryData[] Secrets { get; init; } = [];
}
