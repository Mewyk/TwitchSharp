using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents warn data within a channel.moderate v2 event.</summary>
public sealed record ModerateWarnData
{
    /// <summary>The user ID of the warned user.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login of the warned user.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the warned user.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The reason given for the warning.</summary>
    [JsonPropertyName("reason")]
    public string Reason { get; init; } = string.Empty;

    /// <summary>The chat rules cited in the warning, or null if none were cited.</summary>
    [JsonPropertyName("chat_rules_cited")]
    public string[]? ChatRulesCited { get; init; }
}
