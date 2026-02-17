using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents gift paid upgrade data for a chat notification.</summary>
public sealed record ChatNotificationGiftPaidUpgradeData
{
    /// <summary>Whether the original gifter is anonymous.</summary>
    [JsonPropertyName("gifter_is_anonymous")]
    public bool GifterIsAnonymous { get; init; }

    /// <summary>The gifter's user ID.</summary>
    [JsonPropertyName("gifter_user_id")]
    public string? GifterUserId { get; init; }

    /// <summary>The gifter's user display name.</summary>
    [JsonPropertyName("gifter_user_name")]
    public string? GifterUserName { get; init; }

    /// <summary>The gifter's user login.</summary>
    [JsonPropertyName("gifter_user_login")]
    public string? GifterUserLogin { get; init; }
}
