using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a user's entry on the Bits leaderboard.
/// </summary>
public sealed record BitsLeaderboardEntryData
{
    /// <summary>The user's ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The user's position on the leaderboard.</summary>
    [JsonPropertyName("rank")]
    public int Rank { get; init; }

    /// <summary>The number of Bits the user has cheered.</summary>
    [JsonPropertyName("score")]
    public int Score { get; init; }
}
