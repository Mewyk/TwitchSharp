using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Nested data for an individual Drop entitlement grant.</summary>
public sealed record DropEntitlementData
{
    /// <summary>The organization identifier that owns the game with Drops enabled.</summary>
    [JsonPropertyName("organization_id")]
    public string OrganizationId { get; init; } = string.Empty;

    /// <summary>The Twitch category identifier of the game being played when this benefit was entitled.</summary>
    [JsonPropertyName("category_id")]
    public string CategoryId { get; init; } = string.Empty;

    /// <summary>The category name.</summary>
    [JsonPropertyName("category_name")]
    public string CategoryName { get; init; } = string.Empty;

    /// <summary>The campaign identifier associated with this entitlement.</summary>
    [JsonPropertyName("campaign_id")]
    public string CampaignId { get; init; } = string.Empty;

    /// <summary>The user identifier of the user who was granted the entitlement.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The display name of the user who was granted the entitlement.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The login name of the user who was granted the entitlement.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The unique identifier of the entitlement.</summary>
    [JsonPropertyName("entitlement_id")]
    public string EntitlementId { get; init; } = string.Empty;

    /// <summary>The benefit identifier.</summary>
    [JsonPropertyName("benefit_id")]
    public string BenefitId { get; init; } = string.Empty;

    /// <summary>The timestamp when this entitlement was granted.</summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = string.Empty;
}
