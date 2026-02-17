using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a blocked term that was found in a held message.</summary>
public sealed record AutomodBlockedTermFoundData
{
    /// <summary>The identifier of the blocked term.</summary>
    [JsonPropertyName("term_id")]
    public string TermId { get; init; } = string.Empty;

    /// <summary>The user ID of the broadcaster who owns the blocked term.</summary>
    [JsonPropertyName("owner_broadcaster_user_id")]
    public string OwnerBroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The login name of the broadcaster who owns the blocked term.</summary>
    [JsonPropertyName("owner_broadcaster_user_login")]
    public string OwnerBroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The display name of the broadcaster who owns the blocked term.</summary>
    [JsonPropertyName("owner_broadcaster_user_name")]
    public string OwnerBroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The boundary position of the blocked term in the message.</summary>
    [JsonPropertyName("boundary")]
    public AutomodBoundaryData Boundary { get; init; } = new();
}
