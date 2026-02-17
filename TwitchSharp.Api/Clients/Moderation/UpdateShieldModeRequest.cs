using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Update Shield Mode Status endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record UpdateShieldModeRequest
{
    /// <summary>Whether to activate (true) or deactivate (false) Shield Mode. Required.</summary>
    [JsonPropertyName("is_active")]
    public bool IsActive { get; init; }
}
