using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a game/category returned by the Get Top Games and Get Games endpoints.
/// </summary>
public sealed record GameData
{
    /// <summary>The game's ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The game's name.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>The URL template for the game's box art. Contains {width} and {height} placeholders.</summary>
    [JsonPropertyName("box_art_url")]
    public string BoxArtUrl { get; init; } = string.Empty;

    /// <summary>The game's IGDB ID.</summary>
    [JsonPropertyName("igdb_id")]
    public string IgdbId { get; init; } = string.Empty;
}
