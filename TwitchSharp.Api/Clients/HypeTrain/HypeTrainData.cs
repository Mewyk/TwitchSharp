using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a currently active Hype Train.
/// </summary>
public sealed record HypeTrainData
{
    /// <summary>The Hype Train ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The current level of the Hype Train.</summary>
    [JsonPropertyName("level")]
    public int Level { get; init; }

    /// <summary>Total points contributed to the Hype Train.</summary>
    [JsonPropertyName("total")]
    public int Total { get; init; }

    /// <summary>The number of points contributed to the Hype Train at the current level.</summary>
    [JsonPropertyName("progress")]
    public int Progress { get; init; }

    /// <summary>The number of points required to reach the next level.</summary>
    [JsonPropertyName("goal")]
    public int Goal { get; init; }

    /// <summary>The contributors with the most points contributed.</summary>
    [JsonPropertyName("top_contributions")]
    public HypeTrainContributionData[]? TopContributions { get; init; }

    /// <summary>List of broadcasters participating in the shared Hype Train. Null if not shared.</summary>
    [JsonPropertyName("shared_train_participants")]
    public HypeTrainParticipantData[]? SharedTrainParticipants { get; init; }

    /// <summary>The time when the Hype Train started (ISO 8601 format).</summary>
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;

    /// <summary>The time when the Hype Train expires (ISO 8601 format).</summary>
    [JsonPropertyName("expires_at")]
    public string ExpiresAt { get; init; } = string.Empty;

    /// <summary>The type of the Hype Train. Possible values: treasure, golden_kappa, regular.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>Whether the Hype Train is shared with other broadcasters.</summary>
    [JsonPropertyName("is_shared_train")]
    public bool IsSharedTrain { get; init; }
}
