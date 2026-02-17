using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Create Stream Marker endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record CreateStreamMarkerRequest
{
    /// <summary>The ID of the broadcaster whose stream to mark. Required.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>An optional description of the marker (max 140 characters).</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }
}
