using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Add Suspicious Status endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record AddSuspiciousStatusRequest
{
    /// <summary>The ID of the user being given the suspicious status. Required.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The suspicious status type (ACTIVE_MONITORING or RESTRICTED). Required.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;
}
