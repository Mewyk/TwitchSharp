using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a content classification label.
/// </summary>
public sealed record ContentClassificationLabelData
{
    /// <summary>A unique identifier for the content classification label.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>A description of the content classification label.</summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    /// <summary>The localized name of the content classification label.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}
