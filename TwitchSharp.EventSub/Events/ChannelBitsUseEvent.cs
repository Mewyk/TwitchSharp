using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a channel bits use event.</summary>
public sealed record ChannelBitsUseEvent
{
    /// <summary>The broadcaster's user identifier.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The user identifier of the user who used Bits.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The login name of the user who used Bits.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The display name of the user who used Bits.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The number of Bits used.</summary>
    [JsonPropertyName("bits")]
    public int Bits { get; init; }

    /// <summary>The type of Bits use (e.g., cheer, power_up).</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The message associated with the Bits use. Null for power-ups without messages.</summary>
    [JsonPropertyName("message")]
    public ChatMessageData? Message { get; init; }

    /// <summary>The power-up data. Null for standard cheers.</summary>
    [JsonPropertyName("power_up")]
    public BitsUsePowerUpData? PowerUp { get; init; }
}
