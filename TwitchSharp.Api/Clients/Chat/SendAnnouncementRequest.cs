using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Send Chat Announcement endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record SendAnnouncementRequest
{
    /// <summary>The announcement message (max 500 characters). Required.</summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    /// <summary>The announcement color: "blue", "green", "orange", "purple", or "primary".</summary>
    [JsonPropertyName("color")]
    public string? Color { get; init; }
}
