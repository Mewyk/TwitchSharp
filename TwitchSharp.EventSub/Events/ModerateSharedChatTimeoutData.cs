using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents shared chat timeout data within a channel.moderate v2 event.</summary>
public sealed record ModerateSharedChatTimeoutData
{
    /// <summary>The user ID of the timed-out user.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login of the timed-out user.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the timed-out user.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The reason given for the shared chat timeout.</summary>
    [JsonPropertyName("reason")]
    public string Reason { get; init; } = string.Empty;

    /// <summary>The UTC timestamp of when the timeout expires.</summary>
    [JsonPropertyName("expires_at")]
    public string ExpiresAt { get; init; } = string.Empty;
}
