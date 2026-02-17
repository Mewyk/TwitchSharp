using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the result of updating drops entitlements.
/// </summary>
public sealed record UpdateDropsEntitlementData
{
    /// <summary>The update status (INVALID_ID, NOT_FOUND, SUCCESS, UNAUTHORIZED, UPDATE_FAILED).</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>The entitlement IDs that have this status.</summary>
    [JsonPropertyName("ids")]
    public string[]? Ids { get; init; }
}
