using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents blocked term details when a message is held due to a blocked term.</summary>
public sealed record AutomodBlockedTermData
{
    /// <summary>The blocked terms that were found in the message.</summary>
    [JsonPropertyName("terms_found")]
    public AutomodBlockedTermFoundData[] TermsFound { get; init; } = [];
}
