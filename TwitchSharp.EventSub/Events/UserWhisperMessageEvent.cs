using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a user.whisper.message v1 event, fired when a user receives a whisper.</summary>
public sealed record UserWhisperMessageEvent
{
    /// <summary>The user ID of the user sending the whisper.</summary>
    [JsonPropertyName("from_user_id")]
    public string FromUserId { get; init; } = string.Empty;

    /// <summary>The user login of the user sending the whisper.</summary>
    [JsonPropertyName("from_user_login")]
    public string FromUserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the user sending the whisper.</summary>
    [JsonPropertyName("from_user_name")]
    public string FromUserName { get; init; } = string.Empty;

    /// <summary>The user ID of the user receiving the whisper.</summary>
    [JsonPropertyName("to_user_id")]
    public string ToUserId { get; init; } = string.Empty;

    /// <summary>The user login of the user receiving the whisper.</summary>
    [JsonPropertyName("to_user_login")]
    public string ToUserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the user receiving the whisper.</summary>
    [JsonPropertyName("to_user_name")]
    public string ToUserName { get; init; } = string.Empty;

    /// <summary>The unique identifier of the whisper message.</summary>
    [JsonPropertyName("whisper_id")]
    public string WhisperId { get; init; } = string.Empty;

    /// <summary>The whisper message body content.</summary>
    [JsonPropertyName("whisper")]
    public WhisperBodyData Whisper { get; init; } = new();
}
