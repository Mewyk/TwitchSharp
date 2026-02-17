using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents badge data for a chat user.</summary>
public sealed record ChatBadgeData
{
    /// <summary>The badge set identifier.</summary>
    [JsonPropertyName("set_id")]
    public string SetId { get; init; } = string.Empty;

    /// <summary>The badge identifier within the set.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>Additional information about the badge.</summary>
    [JsonPropertyName("info")]
    public string Information { get; init; } = string.Empty;
}
