using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a poll choice.
/// </summary>
public sealed record PollChoiceData
{
    /// <summary>The choice ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The choice text.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>Total votes cast for this choice.</summary>
    [JsonPropertyName("votes")]
    public int Votes { get; init; }

    /// <summary>Votes cast using Channel Points.</summary>
    [JsonPropertyName("channel_points_votes")]
    public int ChannelPointsVotes { get; init; }

    /// <summary>Not used. Always 0.</summary>
    [JsonPropertyName("bits_votes")]
    public int BitsVotes { get; init; }
}
