using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents a channel.channel_points_custom_reward.add, .update, or .remove
/// EventSub event, fired when a custom channel points reward is created,
/// updated, or removed in the specified channel.
/// </summary>
public sealed record ChannelPointsCustomRewardEvent
{
    /// <summary>The reward identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>Whether the reward is currently enabled.</summary>
    [JsonPropertyName("is_enabled")]
    public bool IsEnabled { get; init; }

    /// <summary>Whether the reward is currently paused. Viewers cannot redeem paused rewards.</summary>
    [JsonPropertyName("is_paused")]
    public bool IsPaused { get; init; }

    /// <summary>Whether the reward is currently in stock. Viewers cannot redeem out-of-stock rewards.</summary>
    [JsonPropertyName("is_in_stock")]
    public bool IsInStock { get; init; }

    /// <summary>The title of the reward.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>The cost of the reward in channel points.</summary>
    [JsonPropertyName("cost")]
    public int Cost { get; init; }

    /// <summary>The prompt shown to the viewer when they redeem the reward, if user input is required.</summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; init; } = string.Empty;

    /// <summary>Whether the user must enter information when redeeming the reward.</summary>
    [JsonPropertyName("is_user_input_required")]
    public bool IsUserInputRequired { get; init; }

    /// <summary>Whether redemptions should be set to FULFILLED status immediately when redeemed.</summary>
    [JsonPropertyName("should_redemptions_skip_request_queue")]
    public bool ShouldRedemptionsSkipRequestQueue { get; init; }

    /// <summary>The settings for the maximum number of redemptions allowed per live stream.</summary>
    [JsonPropertyName("max_per_stream_setting")]
    public MaxPerStreamSettingData MaxPerStreamSetting { get; init; } = new();

    /// <summary>The settings for the maximum number of redemptions allowed per user per live stream.</summary>
    [JsonPropertyName("max_per_user_per_stream_setting")]
    public MaxPerUserPerStreamSettingData MaxPerUserPerStreamSetting { get; init; } = new();

    /// <summary>The background color of the reward in Hex format (for example, #00E5CB).</summary>
    [JsonPropertyName("background_color")]
    public string BackgroundColor { get; init; } = string.Empty;

    /// <summary>A set of custom images for the reward, or null if the broadcaster did not upload images.</summary>
    [JsonPropertyName("image")]
    public RewardImageData? Image { get; init; }

    /// <summary>A set of default images for the reward.</summary>
    [JsonPropertyName("default_image")]
    public RewardImageData DefaultImage { get; init; } = new();

    /// <summary>The global cooldown setting for the reward.</summary>
    [JsonPropertyName("global_cooldown_setting")]
    public GlobalCooldownSettingData GlobalCooldownSetting { get; init; } = new();

    /// <summary>The UTC timestamp of when the cooldown expires, or null if not in a cooldown state.</summary>
    [JsonPropertyName("cooldown_expires_at")]
    public string? CooldownExpiresAt { get; init; }

    /// <summary>The number of redemptions redeemed during the current live stream, or null if the stream is not live or the max-per-stream setting is not enabled.</summary>
    [JsonPropertyName("redemptions_redeemed_current_stream")]
    public int? RedemptionsRedeemedCurrentStream { get; init; }
}
