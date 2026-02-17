using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Nested contribution data for hype train events.</summary>
public sealed record HypeTrainContributionData
{
    /// <summary>The user identifier of the contributor.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The login name of the contributor.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The display name of the contributor.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The contribution type (e.g., bits, subscription, other).</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The total amount contributed.</summary>
    [JsonPropertyName("total")]
    public int Total { get; init; }
}
