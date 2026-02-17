using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents an emote returned by Channel Emotes, Global Emotes, Emote Sets, and User Emotes endpoints.
/// </summary>
public sealed record EmoteData
{
    /// <summary>The emote's ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The emote's name (text used in chat).</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>The emote's image URLs at different sizes. Not present in User Emotes responses.</summary>
    [JsonPropertyName("images")]
    public EmoteImagesData? Images { get; init; }

    /// <summary>The subscription tier required to use the emote (channel emotes only).</summary>
    [JsonPropertyName("tier")]
    public string? Tier { get; init; }

    /// <summary>The emote type (e.g., "bitstier", "follower", "subscriptions"). Not present in Global Emotes.</summary>
    [JsonPropertyName("emote_type")]
    public string? EmoteType { get; init; }

    /// <summary>The emote set ID that this emote belongs to. Not present in Global Emotes.</summary>
    [JsonPropertyName("emote_set_id")]
    public string? EmoteSetId { get; init; }

    /// <summary>The user ID of the emote's owner. Present in Emote Sets and User Emotes responses.</summary>
    [JsonPropertyName("owner_id")]
    public string? OwnerId { get; init; }

    /// <summary>The formats available for this emote: "static" and/or "animated".</summary>
    [JsonPropertyName("format")]
    public string[] Format { get; init; } = [];

    /// <summary>The scales available for this emote: "1.0", "2.0", "3.0".</summary>
    [JsonPropertyName("scale")]
    public string[] Scale { get; init; } = [];

    /// <summary>The theme modes available for this emote: "dark" and/or "light".</summary>
    [JsonPropertyName("theme_mode")]
    public string[] ThemeMode { get; init; } = [];
}
