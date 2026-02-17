using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a user extension returned by the Get User Extensions endpoint.
/// </summary>
public sealed record UserExtensionData
{
    /// <summary>The extension's ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The extension's version.</summary>
    [JsonPropertyName("version")]
    public string Version { get; init; } = string.Empty;

    /// <summary>The extension's name.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>Whether the extension can be activated.</summary>
    [JsonPropertyName("can_activate")]
    public bool CanActivate { get; init; }

    /// <summary>The extension types: "component", "mobile", "overlay", "panel".</summary>
    [JsonPropertyName("type")]
    public string[] Type { get; init; } = [];
}
