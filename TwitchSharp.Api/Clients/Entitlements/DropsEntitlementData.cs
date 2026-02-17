using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a drops entitlement.
/// </summary>
public sealed record DropsEntitlementData
{
    /// <summary>The entitlement ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The benefit ID.</summary>
    [JsonPropertyName("benefit_id")]
    public string BenefitId { get; init; } = string.Empty;

    /// <summary>When the entitlement was granted (RFC3339).</summary>
    [JsonPropertyName("timestamp")]
    public string Timestamp { get; init; } = string.Empty;

    /// <summary>The user's ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The game's ID.</summary>
    [JsonPropertyName("game_id")]
    public string GameId { get; init; } = string.Empty;

    /// <summary>The fulfillment status (CLAIMED or FULFILLED).</summary>
    [JsonPropertyName("fulfillment_status")]
    public string FulfillmentStatus { get; init; } = string.Empty;

    /// <summary>When the entitlement was last updated (RFC3339).</summary>
    [JsonPropertyName("last_updated")]
    public string LastUpdated { get; init; } = string.Empty;
}
