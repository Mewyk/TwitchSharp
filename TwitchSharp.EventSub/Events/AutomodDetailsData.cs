using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents AutoMod details when a message is held by AutoMod.</summary>
public sealed record AutomodDetailsData
{
    /// <summary>The AutoMod category that flagged the message.</summary>
    [JsonPropertyName("category")]
    public string Category { get; init; } = string.Empty;

    /// <summary>The level of severity assigned by AutoMod.</summary>
    [JsonPropertyName("level")]
    public int Level { get; init; }

    /// <summary>The boundary positions of the flagged text.</summary>
    [JsonPropertyName("boundaries")]
    public AutomodBoundaryData[] Boundaries { get; init; } = [];
}
