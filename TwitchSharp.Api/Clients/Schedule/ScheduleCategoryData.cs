using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a schedule segment's category.
/// </summary>
public sealed record ScheduleCategoryData
{
    /// <summary>The category ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The category name.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}
