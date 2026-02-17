using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents position boundaries identifying the flagged portion of text.</summary>
public sealed record AutomodBoundaryData
{
    /// <summary>The starting position of the flagged text.</summary>
    [JsonPropertyName("start_pos")]
    public int StartPos { get; init; }

    /// <summary>The ending position of the flagged text.</summary>
    [JsonPropertyName("end_pos")]
    public int EndPos { get; init; }
}
