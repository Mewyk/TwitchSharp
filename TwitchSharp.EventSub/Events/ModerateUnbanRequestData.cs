using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents unban request data within a channel.moderate v2 event.</summary>
public sealed record ModerateUnbanRequestData
{
    /// <summary>Whether the unban request was approved.</summary>
    [JsonPropertyName("is_approved")]
    public bool IsApproved { get; init; }

    /// <summary>The user ID of the user who requested to be unbanned.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login of the user who requested to be unbanned.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the user who requested to be unbanned.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;
}
