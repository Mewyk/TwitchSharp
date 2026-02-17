using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Modify Channel Information endpoint.
/// All fields are optional; only specified fields are updated.
/// </summary>
[GenerateWithMethods]
public sealed partial record ModifyChannelInformationRequest
{
    /// <summary>The game/category ID. Use "0" or "" to unset.</summary>
    [JsonPropertyName("game_id")]
    public string? GameId { get; init; }

    /// <summary>The broadcaster's preferred language (ISO 639-1 two-letter code).</summary>
    [JsonPropertyName("broadcaster_language")]
    public string? BroadcasterLanguage { get; init; }

    /// <summary>The stream title.</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>The stream delay in seconds (max 900, partners only).</summary>
    [JsonPropertyName("delay")]
    public int? Delay { get; init; }

    /// <summary>The tags to apply to the channel (max 10, each max 25 characters).</summary>
    [JsonPropertyName("tags")]
    public string[]? Tags { get; init; }

    /// <summary>The content classification labels to apply.</summary>
    [JsonPropertyName("content_classification_labels")]
    public ContentClassificationLabel[]? ContentClassificationLabels { get; init; }

    /// <summary>Whether the channel has branded content.</summary>
    [JsonPropertyName("is_branded_content")]
    public bool? IsBrandedContent { get; init; }
}
