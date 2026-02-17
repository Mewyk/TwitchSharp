using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents an active extension slot in a user's extension configuration.
/// </summary>
public sealed record ActiveExtensionSlotData
{
    /// <summary>Whether the extension is active in this slot.</summary>
    [JsonPropertyName("active")]
    public bool Active { get; init; }

    /// <summary>The extension's ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The extension's version.</summary>
    [JsonPropertyName("version")]
    public string Version { get; init; } = string.Empty;

    /// <summary>The extension's name.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>The x-coordinate of the extension (component slots only).</summary>
    [JsonPropertyName("x")]
    public int? X { get; init; }

    /// <summary>The y-coordinate of the extension (component slots only).</summary>
    [JsonPropertyName("y")]
    public int? Y { get; init; }
}
