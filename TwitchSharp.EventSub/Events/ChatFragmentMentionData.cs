using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents mention data within a chat fragment.</summary>
public sealed record ChatFragmentMentionData
{
    /// <summary>The mentioned user's ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The mentioned user's login.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The mentioned user's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;
}
