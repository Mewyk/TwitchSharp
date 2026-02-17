using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents an individual extension JWT secret entry.
/// </summary>
public sealed record ExtensionSecretEntryData
{
    /// <summary>The raw secret used for JWT encoding.</summary>
    [JsonPropertyName("content")]
    public string Content { get; init; } = string.Empty;

    /// <summary>UTC date/time (RFC3339) when this secret becomes active.</summary>
    [JsonPropertyName("active_at")]
    public string ActiveAt { get; init; } = string.Empty;

    /// <summary>UTC date/time (RFC3339) when this secret expires.</summary>
    [JsonPropertyName("expires_at")]
    public string ExpiresAt { get; init; } = string.Empty;
}
