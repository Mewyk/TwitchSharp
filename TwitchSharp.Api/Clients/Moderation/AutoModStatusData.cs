using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the result of an AutoMod message check.
/// </summary>
public sealed record AutoModStatusData
{
    /// <summary>The caller-defined message ID.</summary>
    [JsonPropertyName("msg_id")]
    public string MessageId { get; init; } = string.Empty;

    /// <summary>Whether Twitch would approve the message for chat.</summary>
    [JsonPropertyName("is_permitted")]
    public bool IsPermitted { get; init; }
}
