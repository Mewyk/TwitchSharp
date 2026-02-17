using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a contribution to a Hype Train.
/// </summary>
public sealed record HypeTrainContributionData
{
    /// <summary>The ID of the user that made the contribution.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The contribution method. Possible values: bits, subscription, other.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The total number of points contributed for this type.</summary>
    [JsonPropertyName("total")]
    public int Total { get; init; }
}
