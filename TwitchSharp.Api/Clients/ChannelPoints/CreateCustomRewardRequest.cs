using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Create Custom Rewards endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record CreateCustomRewardRequest
{
    /// <summary>The custom reward's title (max 45 characters). Required.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The cost of the reward, in Channel Points (minimum 1). Required.</summary>
    [JsonPropertyName("cost")]
    public long Cost { get; init; }

    /// <summary>The prompt shown to the viewer when they redeem the reward (max 200 characters).</summary>
    [JsonPropertyName("prompt")]
    public string? Prompt { get; init; }

    /// <summary>Whether the reward is enabled. Default: true.</summary>
    [JsonPropertyName("is_enabled")]
    public bool? IsEnabled { get; init; }

    /// <summary>The background color in Hex format (e.g., #9147FF).</summary>
    [JsonPropertyName("background_color")]
    public string? BackgroundColor { get; init; }

    /// <summary>Whether the user needs to enter information when redeeming. Default: false.</summary>
    [JsonPropertyName("is_user_input_required")]
    public bool? IsUserInputRequired { get; init; }

    /// <summary>Whether to limit the maximum number of redemptions per live stream. Default: false.</summary>
    [JsonPropertyName("is_max_per_stream_enabled")]
    public bool? IsMaxPerStreamEnabled { get; init; }

    /// <summary>The maximum number of redemptions allowed per live stream (minimum 1).</summary>
    [JsonPropertyName("max_per_stream")]
    public int? MaxPerStream { get; init; }

    /// <summary>Whether to limit the maximum number of redemptions per user per stream. Default: false.</summary>
    [JsonPropertyName("is_max_per_user_per_stream_enabled")]
    public bool? IsMaxPerUserPerStreamEnabled { get; init; }

    /// <summary>The maximum number of redemptions allowed per user per stream (minimum 1).</summary>
    [JsonPropertyName("max_per_user_per_stream")]
    public int? MaxPerUserPerStream { get; init; }

    /// <summary>Whether to apply a cooldown period between redemptions. Default: false.</summary>
    [JsonPropertyName("is_global_cooldown_enabled")]
    public bool? IsGlobalCooldownEnabled { get; init; }

    /// <summary>The cooldown period in seconds (minimum 1, minimum 60 for Twitch UX).</summary>
    [JsonPropertyName("global_cooldown_seconds")]
    public int? GlobalCooldownSeconds { get; init; }

    /// <summary>Whether redemptions should be set to FULFILLED immediately. Default: false.</summary>
    [JsonPropertyName("should_redemptions_skip_request_queue")]
    public bool? ShouldRedemptionsSkipRequestQueue { get; init; }
}
