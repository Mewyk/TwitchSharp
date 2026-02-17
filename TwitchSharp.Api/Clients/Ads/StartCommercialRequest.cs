using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Start Commercial endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record StartCommercialRequest
{
    /// <summary>The ID of the partner or affiliate broadcaster. Must match the user ID in the OAuth token. Required.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The length of the commercial in seconds. Maximum: 180.</summary>
    [JsonPropertyName("length")]
    public int Length { get; init; }
}
