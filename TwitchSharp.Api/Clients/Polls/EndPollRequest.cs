using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the End Poll endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record EndPollRequest
{
    /// <summary>The broadcaster's ID. Must match the user ID in the token. Required.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The poll ID to end. Required.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The status to set: "TERMINATED" or "ARCHIVED". Required.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;
}
