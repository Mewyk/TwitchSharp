using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.cheer v1 EventSub event, fired when a user cheers
/// in the specified broadcaster's channel.
/// </summary>
public sealed record ChannelCheerEvent
{
    /// <summary>Whether the cheer was sent anonymously.</summary>
    [JsonPropertyName("is_anonymous")]
    public bool IsAnonymous { get; init; }

    /// <summary>The user ID of the cheerer, or null if anonymous.</summary>
    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }

    /// <summary>The user login name of the cheerer, or null if anonymous.</summary>
    [JsonPropertyName("user_login")]
    public string? UserLogin { get; init; }

    /// <summary>The user display name of the cheerer, or null if anonymous.</summary>
    [JsonPropertyName("user_name")]
    public string? UserName { get; init; }

    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The message sent with the cheer.</summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    /// <summary>The number of bits cheered.</summary>
    [JsonPropertyName("bits")]
    public int Bits { get; init; }
}
