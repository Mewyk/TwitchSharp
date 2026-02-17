using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a channel.moderate v2 event, fired when a moderator performs a moderation action in a channel.</summary>
public sealed record ChannelModerateEvent
{
    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The moderator's user ID who performed the action.</summary>
    [JsonPropertyName("moderator_user_id")]
    public string ModeratorUserId { get; init; } = string.Empty;

    /// <summary>The moderator's user login who performed the action.</summary>
    [JsonPropertyName("moderator_user_login")]
    public string ModeratorUserLogin { get; init; } = string.Empty;

    /// <summary>The moderator's user display name who performed the action.</summary>
    [JsonPropertyName("moderator_user_name")]
    public string ModeratorUserName { get; init; } = string.Empty;

    /// <summary>The moderation action that was performed.</summary>
    [JsonPropertyName("action")]
    public string Action { get; init; } = string.Empty;

    /// <summary>The follower-only mode data if the action is followers.</summary>
    [JsonPropertyName("followers")]
    public ModerateFollowersData? Followers { get; init; }

    /// <summary>The slow mode data if the action is slow.</summary>
    [JsonPropertyName("slow")]
    public ModerateSlowData? Slow { get; init; }

    /// <summary>The VIP data if the action is vip.</summary>
    [JsonPropertyName("vip")]
    public ModerateUserData? Vip { get; init; }

    /// <summary>The un-VIP data if the action is unvip.</summary>
    [JsonPropertyName("unvip")]
    public ModerateUserData? Unvip { get; init; }

    /// <summary>The mod data if the action is mod.</summary>
    [JsonPropertyName("mod")]
    public ModerateUserData? Mod { get; init; }

    /// <summary>The unmod data if the action is unmod.</summary>
    [JsonPropertyName("unmod")]
    public ModerateUserData? Unmod { get; init; }

    /// <summary>The ban data if the action is ban.</summary>
    [JsonPropertyName("ban")]
    public ModerateBanData? Ban { get; init; }

    /// <summary>The unban data if the action is unban.</summary>
    [JsonPropertyName("unban")]
    public ModerateUserData? Unban { get; init; }

    /// <summary>The timeout data if the action is timeout.</summary>
    [JsonPropertyName("timeout")]
    public ModerateTimeoutData? Timeout { get; init; }

    /// <summary>The untimeout data if the action is untimeout.</summary>
    [JsonPropertyName("untimeout")]
    public ModerateUserData? Untimeout { get; init; }

    /// <summary>The raid data if the action is raid.</summary>
    [JsonPropertyName("raid")]
    public ModerateUserData? Raid { get; init; }

    /// <summary>The unraid data if the action is unraid.</summary>
    [JsonPropertyName("unraid")]
    public ModerateUserData? Unraid { get; init; }

    /// <summary>The message deletion data if the action is delete.</summary>
    [JsonPropertyName("delete")]
    public ModerateDeleteData? Delete { get; init; }

    /// <summary>The AutoMod terms data if the action is automod_terms.</summary>
    [JsonPropertyName("automod_terms")]
    public ModerateAutomodTermsData? AutomodTerms { get; init; }

    /// <summary>The unban request data if the action is unban_request.</summary>
    [JsonPropertyName("unban_request")]
    public ModerateUnbanRequestData? UnbanRequest { get; init; }

    /// <summary>The warn data if the action is warn.</summary>
    [JsonPropertyName("warn")]
    public ModerateWarnData? Warn { get; init; }

    /// <summary>The shared chat ban data if the action is shared_chat_ban.</summary>
    [JsonPropertyName("shared_chat_ban")]
    public ModerateSharedChatBanData? SharedChatBan { get; init; }

    /// <summary>The shared chat unban data if the action is shared_chat_unban.</summary>
    [JsonPropertyName("shared_chat_unban")]
    public ModerateUserData? SharedChatUnban { get; init; }

    /// <summary>The shared chat timeout data if the action is shared_chat_timeout.</summary>
    [JsonPropertyName("shared_chat_timeout")]
    public ModerateSharedChatTimeoutData? SharedChatTimeout { get; init; }

    /// <summary>The shared chat untimeout data if the action is shared_chat_untimeout.</summary>
    [JsonPropertyName("shared_chat_untimeout")]
    public ModerateUserData? SharedChatUntimeout { get; init; }

    /// <summary>The shared chat message deletion data if the action is shared_chat_delete.</summary>
    [JsonPropertyName("shared_chat_delete")]
    public ModerateSharedChatDeleteData? SharedChatDelete { get; init; }
}
