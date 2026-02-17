using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents channel information returned by the Get Channel Information endpoint.
/// </summary>
public sealed record ChannelInformationData
{
    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_login")]
    public string BroadcasterLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_name")]
    public string BroadcasterName { get; init; } = string.Empty;

    /// <summary>The broadcaster's preferred language (ISO 639-1 two-letter code).</summary>
    [JsonPropertyName("broadcaster_language")]
    public string BroadcasterLanguage { get; init; } = string.Empty;

    /// <summary>The name of the game being played.</summary>
    [JsonPropertyName("game_name")]
    public string GameName { get; init; } = string.Empty;

    /// <summary>The ID of the game being played.</summary>
    [JsonPropertyName("game_id")]
    public string GameId { get; init; } = string.Empty;

    /// <summary>The channel's stream title.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The stream delay in seconds (partners only).</summary>
    [JsonPropertyName("delay")]
    public int Delay { get; init; }

    /// <summary>The tags applied to the channel.</summary>
    [JsonPropertyName("tags")]
    public string[] Tags { get; init; } = [];

    /// <summary>The content classification labels applied to the channel.</summary>
    [JsonPropertyName("content_classification_labels")]
    public string[] ContentClassificationLabels { get; init; } = [];

    /// <summary>Whether the channel has branded content enabled.</summary>
    [JsonPropertyName("is_branded_content")]
    public bool IsBrandedContent { get; init; }
}
