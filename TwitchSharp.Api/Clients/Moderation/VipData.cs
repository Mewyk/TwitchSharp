using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a channel VIP.
/// </summary>
public sealed record VipData
{
    /// <summary>The VIP's user ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The VIP's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The VIP's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;
}
