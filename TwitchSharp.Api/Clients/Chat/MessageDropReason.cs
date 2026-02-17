using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the reason a chat message was dropped.
/// </summary>
public sealed record MessageDropReason
{
    /// <summary>The drop reason code.</summary>
    [JsonPropertyName("code")]
    public string Code { get; init; } = string.Empty;

    /// <summary>A human-readable message describing the reason.</summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;
}
