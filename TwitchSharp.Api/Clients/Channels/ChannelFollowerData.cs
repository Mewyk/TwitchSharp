using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a channel follower returned by the Get Channel Followers endpoint.
/// </summary>
public sealed record ChannelFollowerData
{
    /// <summary>The follower's user ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The follower's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The follower's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The date and time the user followed the broadcaster.</summary>
    [JsonPropertyName("followed_at")]
    public DateTimeOffset FollowedAt { get; init; }
}
