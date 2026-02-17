using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a reward within a redemption response.
/// </summary>
public sealed record RedemptionRewardData
{
    /// <summary>The ID that uniquely identifies the redeemed reward.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The reward's title.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The prompt displayed to the viewer if user input is required.</summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; init; } = string.Empty;

    /// <summary>The reward's cost, in Channel Points.</summary>
    [JsonPropertyName("cost")]
    public long Cost { get; init; }
}
