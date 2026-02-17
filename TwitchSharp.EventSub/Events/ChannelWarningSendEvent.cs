using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.warning.send v1 EventSub event, fired when
/// a moderator sends a warning to a user in the specified broadcaster's channel.
/// </summary>
public sealed record ChannelWarningSendEvent
{
    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The moderator's user ID who sent the warning.</summary>
    [JsonPropertyName("moderator_user_id")]
    public string ModeratorUserId { get; init; } = string.Empty;

    /// <summary>The moderator's user login name who sent the warning.</summary>
    [JsonPropertyName("moderator_user_login")]
    public string ModeratorUserLogin { get; init; } = string.Empty;

    /// <summary>The moderator's user display name who sent the warning.</summary>
    [JsonPropertyName("moderator_user_name")]
    public string ModeratorUserName { get; init; } = string.Empty;

    /// <summary>The user ID of the user who was warned.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login name of the user who was warned.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the user who was warned.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The reason given for the warning.</summary>
    [JsonPropertyName("reason")]
    public string Reason { get; init; } = string.Empty;

    /// <summary>The chat rules cited in the warning, or null if no rules were cited.</summary>
    [JsonPropertyName("chat_rules_cited")]
    public string[]? ChatRulesCited { get; init; }
}
