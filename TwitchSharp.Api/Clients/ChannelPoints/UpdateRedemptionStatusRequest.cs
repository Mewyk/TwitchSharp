using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Update Redemption Status endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record UpdateRedemptionStatusRequest
{
    /// <summary>The status to set the redemption to (CANCELED or FULFILLED). Required.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;
}
