using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the data returned when creating a clip.
/// </summary>
public sealed record CreateClipData
{
    /// <summary>The unique clip ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The URL to edit the clip title and publish it. Valid for 24 hours or until published.</summary>
    [JsonPropertyName("edit_url")]
    public string EditUrl { get; init; } = string.Empty;
}
