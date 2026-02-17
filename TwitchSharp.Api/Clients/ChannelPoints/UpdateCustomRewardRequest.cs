using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Update Custom Reward endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record UpdateCustomRewardRequest
{
    /// <summary>The reward's title (max 45 characters).</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>The prompt shown to the viewer when they redeem the reward (max 200 characters).</summary>
    [JsonPropertyName("prompt")]
    public string? Prompt { get; init; }

    /// <summary>The cost of the reward, in Channel Points (minimum 1).</summary>
    [JsonPropertyName("cost")]
    public long? Cost { get; init; }

    /// <summary>The background color in Hex format (e.g., #00E5CB).</summary>
    [JsonPropertyName("background_color")]
    public string? BackgroundColor { get; init; }

    /// <summary>Whether the reward is enabled.</summary>
    [JsonPropertyName("is_enabled")]
    public bool? IsEnabled { get; init; }

    /// <summary>Whether the user must enter information when redeeming.</summary>
    [JsonPropertyName("is_user_input_required")]
    public bool? IsUserInputRequired { get; init; }

    /// <summary>Whether to limit the maximum number of redemptions per live stream.</summary>
    [JsonPropertyName("is_max_per_stream_enabled")]
    public bool? IsMaxPerStreamEnabled { get; init; }

    /// <summary>The maximum number of redemptions allowed per live stream (minimum 1).</summary>
    [JsonPropertyName("max_per_stream")]
    public long? MaxPerStream { get; init; }

    /// <summary>Whether to limit the maximum number of redemptions per user per stream.</summary>
    [JsonPropertyName("is_max_per_user_per_stream_enabled")]
    public bool? IsMaxPerUserPerStreamEnabled { get; init; }

    /// <summary>The maximum number of redemptions allowed per user per stream.</summary>
    [JsonPropertyName("max_per_user_per_stream")]
    public long? MaxPerUserPerStream { get; init; }

    /// <summary>Whether to apply a cooldown period between redemptions.</summary>
    [JsonPropertyName("is_global_cooldown_enabled")]
    public bool? IsGlobalCooldownEnabled { get; init; }

    /// <summary>The cooldown period in seconds (minimum 1, maximum 604800).</summary>
    [JsonPropertyName("global_cooldown_seconds")]
    public long? GlobalCooldownSeconds { get; init; }

    /// <summary>Whether the reward is paused.</summary>
    [JsonPropertyName("is_paused")]
    public bool? IsPaused { get; init; }

    /// <summary>Whether redemptions should be set to FULFILLED immediately.</summary>
    [JsonPropertyName("should_redemptions_skip_request_queue")]
    public bool? ShouldRedemptionsSkipRequestQueue { get; init; }
}
