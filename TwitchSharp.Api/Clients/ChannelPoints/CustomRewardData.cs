using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a custom channel points reward.
/// </summary>
public sealed record CustomRewardData
{
    /// <summary>The ID that uniquely identifies the broadcaster.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_login")]
    public string BroadcasterLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_name")]
    public string BroadcasterName { get; init; } = string.Empty;

    /// <summary>The ID that uniquely identifies this custom reward.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The title of the reward.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The prompt shown to the viewer when they redeem the reward.</summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; init; } = string.Empty;

    /// <summary>The cost of the reward in Channel Points.</summary>
    [JsonPropertyName("cost")]
    public long Cost { get; init; }

    /// <summary>A set of custom images for the reward, or null if none were uploaded.</summary>
    [JsonPropertyName("image")]
    public CustomRewardImageData? Image { get; init; }

    /// <summary>A set of default images for the reward.</summary>
    [JsonPropertyName("default_image")]
    public CustomRewardImageData? DefaultImage { get; init; }

    /// <summary>The background color to use for the reward in Hex format.</summary>
    [JsonPropertyName("background_color")]
    public string BackgroundColor { get; init; } = string.Empty;

    /// <summary>Whether the reward is enabled.</summary>
    [JsonPropertyName("is_enabled")]
    public bool IsEnabled { get; init; }

    /// <summary>Whether the user must enter information when redeeming the reward.</summary>
    [JsonPropertyName("is_user_input_required")]
    public bool IsUserInputRequired { get; init; }

    /// <summary>Settings for the maximum number of redemptions allowed per live stream.</summary>
    [JsonPropertyName("max_per_stream_setting")]
    public MaxPerStreamSettingData? MaxPerStreamSetting { get; init; }

    /// <summary>Settings for the maximum number of redemptions allowed per user per live stream.</summary>
    [JsonPropertyName("max_per_user_per_stream_setting")]
    public MaxPerUserPerStreamSettingData? MaxPerUserPerStreamSetting { get; init; }

    /// <summary>Settings for the cooldown period between redemptions.</summary>
    [JsonPropertyName("global_cooldown_setting")]
    public GlobalCooldownSettingData? GlobalCooldownSetting { get; init; }

    /// <summary>Whether the reward is currently paused.</summary>
    [JsonPropertyName("is_paused")]
    public bool IsPaused { get; init; }

    /// <summary>Whether the reward is currently in stock.</summary>
    [JsonPropertyName("is_in_stock")]
    public bool IsInStock { get; init; }

    /// <summary>Whether redemptions should be set to FULFILLED status immediately.</summary>
    [JsonPropertyName("should_redemptions_skip_request_queue")]
    public bool ShouldRedemptionsSkipRequestQueue { get; init; }

    /// <summary>The number of redemptions redeemed during the current live stream, or null if not live.</summary>
    [JsonPropertyName("redemptions_redeemed_current_stream")]
    public int? RedemptionsRedeemedCurrentStream { get; init; }

    /// <summary>The timestamp of when the cooldown period expires, or null if not in cooldown.</summary>
    [JsonPropertyName("cooldown_expires_at")]
    public string? CooldownExpiresAt { get; init; }
}
