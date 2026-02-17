using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a broadcaster streaming live with an extension installed or activated.
/// </summary>
public sealed record ExtensionLiveChannelData
{
    /// <summary>The ID of the broadcaster streaming live with the extension.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_name")]
    public string BroadcasterName { get; init; } = string.Empty;

    /// <summary>The name of the category or game being streamed.</summary>
    [JsonPropertyName("game_name")]
    public string GameName { get; init; } = string.Empty;

    /// <summary>The ID of the category or game being streamed.</summary>
    [JsonPropertyName("game_id")]
    public string GameId { get; init; } = string.Empty;

    /// <summary>The title of the broadcaster's stream.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;
}
