using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.update v2 EventSub event, fired when a broadcaster updates
/// their channel properties such as title, language, or category.
/// </summary>
public sealed record ChannelUpdateEvent
{
    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The channel's stream title.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The channel's broadcast language.</summary>
    [JsonPropertyName("language")]
    public string Language { get; init; } = string.Empty;

    /// <summary>The category ID for the channel's current category.</summary>
    [JsonPropertyName("category_id")]
    public string CategoryId { get; init; } = string.Empty;

    /// <summary>The category name for the channel's current category.</summary>
    [JsonPropertyName("category_name")]
    public string CategoryName { get; init; } = string.Empty;

    /// <summary>The content classification labels applied to the channel.</summary>
    [JsonPropertyName("content_classification_labels")]
    public string[] ContentClassificationLabels { get; init; } = [];
}
