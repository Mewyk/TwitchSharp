using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Nested poll choice data for poll events.</summary>
public sealed record PollChoiceData
{
    /// <summary>The choice identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The choice title text.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The number of votes cast using bits.</summary>
    [JsonPropertyName("bits_votes")]
    public int BitsVotes { get; init; }

    /// <summary>The number of votes cast using channel points.</summary>
    [JsonPropertyName("channel_points_votes")]
    public int ChannelPointsVotes { get; init; }

    /// <summary>The total number of votes for this choice.</summary>
    [JsonPropertyName("votes")]
    public int Votes { get; init; }
}
