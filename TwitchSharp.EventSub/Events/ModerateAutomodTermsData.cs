using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents AutoMod terms modification data within a channel.moderate v2 event.</summary>
public sealed record ModerateAutomodTermsData
{
    /// <summary>The action performed on the terms, such as add or remove.</summary>
    [JsonPropertyName("action")]
    public string Action { get; init; } = string.Empty;

    /// <summary>The list type, such as blocked or permitted.</summary>
    [JsonPropertyName("list")]
    public string List { get; init; } = string.Empty;

    /// <summary>The terms that were modified.</summary>
    [JsonPropertyName("terms")]
    public string[] Terms { get; init; } = [];

    /// <summary>Whether the terms were added by AutoMod rather than a moderator.</summary>
    [JsonPropertyName("from_automod")]
    public bool FromAutomod { get; init; }
}
