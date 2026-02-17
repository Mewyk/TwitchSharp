using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents ban data within a channel.moderate v2 event.</summary>
public sealed record ModerateBanData
{
    /// <summary>The user ID of the banned user.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login of the banned user.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the banned user.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The reason given for the ban.</summary>
    [JsonPropertyName("reason")]
    public string Reason { get; init; } = string.Empty;
}
