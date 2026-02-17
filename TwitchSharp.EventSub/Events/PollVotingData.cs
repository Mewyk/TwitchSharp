using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Nested voting configuration data for poll events.</summary>
public sealed record PollVotingData
{
    /// <summary>Whether this voting method is enabled.</summary>
    [JsonPropertyName("is_enabled")]
    public bool IsEnabled { get; init; }

    /// <summary>The cost per additional vote using this method.</summary>
    [JsonPropertyName("amount_per_vote")]
    public int AmountPerVote { get; init; }
}
