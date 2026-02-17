using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Update Drops Entitlements endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record UpdateDropsEntitlementsRequest
{
    /// <summary>The entitlement IDs to update (max 100).</summary>
    [JsonPropertyName("entitlement_ids")]
    public string[]? EntitlementIds { get; init; }

    /// <summary>The fulfillment status to set (CLAIMED or FULFILLED).</summary>
    [JsonPropertyName("fulfillment_status")]
    public string? FulfillmentStatus { get; init; }
}
