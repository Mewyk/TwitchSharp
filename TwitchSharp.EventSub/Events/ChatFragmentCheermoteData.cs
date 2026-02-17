using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents cheermote data within a chat fragment.</summary>
public sealed record ChatFragmentCheermoteData
{
    /// <summary>The cheermote prefix.</summary>
    [JsonPropertyName("prefix")]
    public string Prefix { get; init; } = string.Empty;

    /// <summary>The number of bits cheered.</summary>
    [JsonPropertyName("bits")]
    public int Bits { get; init; }

    /// <summary>The tier of the cheermote.</summary>
    [JsonPropertyName("tier")]
    public int Tier { get; init; }
}
