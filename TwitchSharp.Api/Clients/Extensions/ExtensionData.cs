using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a released extension's information.
/// </summary>
public sealed record ExtensionData
{
    /// <summary>The name of the user or organization that owns the extension.</summary>
    [JsonPropertyName("author_name")]
    public string AuthorName { get; init; } = string.Empty;

    /// <summary>Whether the extension has features that use Bits.</summary>
    [JsonPropertyName("bits_enabled")]
    public bool BitsEnabled { get; init; }

    /// <summary>Whether a user can install the extension on their channel.</summary>
    [JsonPropertyName("can_install")]
    public bool CanInstall { get; init; }

    /// <summary>Where the extension's configuration is stored (hosted, custom, or none).</summary>
    [JsonPropertyName("configuration_location")]
    public string ConfigurationLocation { get; init; } = string.Empty;

    /// <summary>A longer description of the extension.</summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    /// <summary>A URL to the extension's Terms of Service.</summary>
    [JsonPropertyName("eula_tos_url")]
    public string EulaTosUrl { get; init; } = string.Empty;

    /// <summary>Whether the extension can communicate with the channel's chat room.</summary>
    [JsonPropertyName("has_chat_support")]
    public bool HasChatSupport { get; init; }

    /// <summary>A URL to the default icon displayed in the Extensions directory.</summary>
    [JsonPropertyName("icon_url")]
    public string IconUrl { get; init; } = string.Empty;

    /// <summary>URLs to different sizes of the default icon, keyed by size (e.g., "24x24").</summary>
    [JsonPropertyName("icon_urls")]
    public Dictionary<string, string>? IconUrls { get; init; }

    /// <summary>The extension's ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The extension's name.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>A URL to the extension's privacy policy.</summary>
    [JsonPropertyName("privacy_policy_url")]
    public string PrivacyPolicyUrl { get; init; } = string.Empty;

    /// <summary>Whether the extension asks viewers to link their Twitch identity.</summary>
    [JsonPropertyName("request_identity_link")]
    public bool RequestIdentityLink { get; init; }

    /// <summary>URLs to screenshots shown in the Extensions marketplace.</summary>
    [JsonPropertyName("screenshot_urls")]
    public string[]? ScreenshotUrls { get; init; }

    /// <summary>The extension's state (e.g., Released, InTest, Deprecated).</summary>
    [JsonPropertyName("state")]
    public string State { get; init; } = string.Empty;

    /// <summary>Whether the extension can view the user's subscription level (none or optional).</summary>
    [JsonPropertyName("subscriptions_support_level")]
    public string SubscriptionsSupportLevel { get; init; } = string.Empty;

    /// <summary>A short description shown in the Extensions manager discovery splash.</summary>
    [JsonPropertyName("summary")]
    public string Summary { get; init; } = string.Empty;

    /// <summary>The email address for extension support.</summary>
    [JsonPropertyName("support_email")]
    public string SupportEmail { get; init; } = string.Empty;

    /// <summary>The extension's version number.</summary>
    [JsonPropertyName("version")]
    public string Version { get; init; } = string.Empty;

    /// <summary>A brief description displayed on the channel explaining how the extension works.</summary>
    [JsonPropertyName("viewer_summary")]
    public string ViewerSummary { get; init; } = string.Empty;

    /// <summary>View configurations for all extension display types.</summary>
    [JsonPropertyName("views")]
    public ExtensionViewsData? Views { get; init; }

    /// <summary>Allowlisted configuration URLs for the extension.</summary>
    [JsonPropertyName("allowlisted_config_urls")]
    public string[]? AllowlistedConfigUrls { get; init; }

    /// <summary>Allowlisted panel URLs for the extension.</summary>
    [JsonPropertyName("allowlisted_panel_urls")]
    public string[]? AllowlistedPanelUrls { get; init; }
}
