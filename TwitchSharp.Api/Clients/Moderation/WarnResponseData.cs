using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the response from warning a chat user.
/// </summary>
public sealed record WarnResponseData
{
    /// <summary>The ID of the channel in which the warning took effect.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The ID of the warned user.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The ID of the moderator who applied the warning.</summary>
    [JsonPropertyName("moderator_id")]
    public string ModeratorId { get; init; } = string.Empty;

    /// <summary>The reason provided for the warning.</summary>
    [JsonPropertyName("reason")]
    public string Reason { get; init; } = string.Empty;
}
