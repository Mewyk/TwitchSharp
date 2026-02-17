using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents user data shared across multiple channel.moderate v2 action types including vip, unvip, mod, unmod, unban, untimeout, raid, and unraid.</summary>
public sealed record ModerateUserData
{
    /// <summary>The user ID of the affected user.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login of the affected user.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the affected user.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;
}
