using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a followed channel returned by the Get Followed Channels endpoint.
/// </summary>
public sealed record FollowedChannelData
{
    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_login")]
    public string BroadcasterLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_name")]
    public string BroadcasterName { get; init; } = string.Empty;

    /// <summary>The date and time the user followed the broadcaster.</summary>
    [JsonPropertyName("followed_at")]
    public DateTimeOffset FollowedAt { get; init; }
}
