using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents raid data for a chat notification.</summary>
public sealed record ChatNotificationRaidData
{
    /// <summary>The raiding user's ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The raiding user's login.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The raiding user's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The number of viewers in the raid.</summary>
    [JsonPropertyName("viewer_count")]
    public int ViewerCount { get; init; }

    /// <summary>The raiding user's profile image URL.</summary>
    [JsonPropertyName("profile_image_url")]
    public string ProfileImageUrl { get; init; } = string.Empty;
}
