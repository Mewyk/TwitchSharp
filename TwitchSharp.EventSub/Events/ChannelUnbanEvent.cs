using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.unban v1 EventSub event, fired when a user is unbanned
/// in the specified broadcaster's channel.
/// </summary>
public sealed record ChannelUnbanEvent
{
    /// <summary>The user ID of the unbanned user.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login name of the unbanned user.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the unbanned user.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The moderator's user ID who performed the unban.</summary>
    [JsonPropertyName("moderator_user_id")]
    public string ModeratorUserId { get; init; } = string.Empty;

    /// <summary>The moderator's user login name who performed the unban.</summary>
    [JsonPropertyName("moderator_user_login")]
    public string ModeratorUserLogin { get; init; } = string.Empty;

    /// <summary>The moderator's user display name who performed the unban.</summary>
    [JsonPropertyName("moderator_user_name")]
    public string ModeratorUserName { get; init; } = string.Empty;
}
