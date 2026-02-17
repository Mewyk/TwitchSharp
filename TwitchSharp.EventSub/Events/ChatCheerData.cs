using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents cheer data associated with a chat message.</summary>
public sealed record ChatCheerData
{
    /// <summary>The number of bits cheered.</summary>
    [JsonPropertyName("bits")]
    public int Bits { get; init; }
}
