namespace TwitchSharp.Api.Authentication;

/// <summary>
/// String constants for all Twitch OAuth scopes.
/// Use these when building authorization URLs and registering subscriptions
/// instead of raw strings.
/// </summary>
public static class TwitchScopes
{
    // Analytics

    /// <summary>View analytics data for the Twitch Extensions owned by the authenticated account.</summary>
    public const string AnalyticsReadExtensions = "analytics:read:extensions";

    /// <summary>View analytics data for the games owned by the authenticated account.</summary>
    public const string AnalyticsReadGames = "analytics:read:games";

    // Bits

    /// <summary>View Bits information for a channel.</summary>
    public const string BitsRead = "bits:read";

    // Channel

    /// <summary>Joins your channel's chatroom as a bot user, and perform chat-related actions.</summary>
    public const string ChannelBot = "channel:bot";

    /// <summary>Manage ads schedule on a channel.</summary>
    public const string ChannelManageAds = "channel:manage:ads";

    /// <summary>Read the ads schedule and details on your channel.</summary>
    public const string ChannelReadAds = "channel:read:ads";

    /// <summary>Manage a channel's broadcast configuration, including stream markers and tags.</summary>
    public const string ChannelManageBroadcast = "channel:manage:broadcast";

    /// <summary>Read charity campaign details and user donations on your channel.</summary>
    public const string ChannelReadCharity = "channel:read:charity";

    /// <summary>Manage Clips for a channel.</summary>
    public const string ChannelManageClips = "channel:manage:clips";

    /// <summary>Run commercials on a channel.</summary>
    public const string ChannelEditCommercial = "channel:edit:commercial";

    /// <summary>View a list of users with the editor role for a channel.</summary>
    public const string ChannelReadEditors = "channel:read:editors";

    /// <summary>Manage a channel's Extension configuration, including activating Extensions.</summary>
    public const string ChannelManageExtensions = "channel:manage:extensions";

    /// <summary>View Creator Goals for a channel.</summary>
    public const string ChannelReadGoals = "channel:read:goals";

    /// <summary>Read Guest Star details for your channel.</summary>
    public const string ChannelReadGuestStar = "channel:read:guest_star";

    /// <summary>Manage Guest Star for your channel.</summary>
    public const string ChannelManageGuestStar = "channel:manage:guest_star";

    /// <summary>View Hype Train information for a channel.</summary>
    public const string ChannelReadHypeTrain = "channel:read:hype_train";

    /// <summary>Add or remove the moderator role from users in your channel.</summary>
    public const string ChannelManageModerators = "channel:manage:moderators";

    /// <summary>View a channel's polls.</summary>
    public const string ChannelReadPolls = "channel:read:polls";

    /// <summary>Manage a channel's polls.</summary>
    public const string ChannelManagePolls = "channel:manage:polls";

    /// <summary>View a channel's Channel Points Predictions.</summary>
    public const string ChannelReadPredictions = "channel:read:predictions";

    /// <summary>Manage a channel's Channel Points Predictions.</summary>
    public const string ChannelManagePredictions = "channel:manage:predictions";

    /// <summary>Manage a channel raiding another channel.</summary>
    public const string ChannelManageRaids = "channel:manage:raids";

    /// <summary>View Channel Points custom rewards and their redemptions on a channel.</summary>
    public const string ChannelReadRedemptions = "channel:read:redemptions";

    /// <summary>Manage Channel Points custom rewards and their redemptions on a channel.</summary>
    public const string ChannelManageRedemptions = "channel:manage:redemptions";

    /// <summary>Manage a channel's stream schedule.</summary>
    public const string ChannelManageSchedule = "channel:manage:schedule";

    /// <summary>View an authorized user's stream key.</summary>
    public const string ChannelReadStreamKey = "channel:read:stream_key";

    /// <summary>View subscribers to a channel and check subscription status.</summary>
    public const string ChannelReadSubscriptions = "channel:read:subscriptions";

    /// <summary>Manage a channel's videos, including deleting videos.</summary>
    public const string ChannelManageVideos = "channel:manage:videos";

    /// <summary>Read the list of VIPs in your channel.</summary>
    public const string ChannelReadVips = "channel:read:vips";

    /// <summary>Add or remove the VIP role from users in your channel.</summary>
    public const string ChannelManageVips = "channel:manage:vips";

    /// <summary>Perform moderation actions in a channel.</summary>
    public const string ChannelModerate = "channel:moderate";

    // Clips

    /// <summary>Manage Clips for a channel.</summary>
    public const string ClipsEdit = "clips:edit";

    // Editor

    /// <summary>Manage Clips as an editor.</summary>
    public const string EditorManageClips = "editor:manage:clips";

    // Moderation

    /// <summary>View channel moderation data: Moderators, Bans, Timeouts, and Automod settings.</summary>
    public const string ModerationRead = "moderation:read";

    /// <summary>Send announcements in channels where you have the moderator role.</summary>
    public const string ModeratorManageAnnouncements = "moderator:manage:announcements";

    /// <summary>Manage messages held for review by AutoMod.</summary>
    public const string ModeratorManageAutomod = "moderator:manage:automod";

    /// <summary>View a broadcaster's AutoMod settings.</summary>
    public const string ModeratorReadAutomodSettings = "moderator:read:automod_settings";

    /// <summary>Manage a broadcaster's AutoMod settings.</summary>
    public const string ModeratorManageAutomodSettings = "moderator:manage:automod_settings";

    /// <summary>Read the list of bans or unbans where you have the moderator role.</summary>
    public const string ModeratorReadBannedUsers = "moderator:read:banned_users";

    /// <summary>Ban and unban users.</summary>
    public const string ModeratorManageBannedUsers = "moderator:manage:banned_users";

    /// <summary>View a broadcaster's list of blocked terms.</summary>
    public const string ModeratorReadBlockedTerms = "moderator:read:blocked_terms";

    /// <summary>Read deleted chat messages where you have the moderator role.</summary>
    public const string ModeratorReadChatMessages = "moderator:read:chat_messages";

    /// <summary>Manage a broadcaster's list of blocked terms.</summary>
    public const string ModeratorManageBlockedTerms = "moderator:manage:blocked_terms";

    /// <summary>Delete chat messages where you have the moderator role.</summary>
    public const string ModeratorManageChatMessages = "moderator:manage:chat_messages";

    /// <summary>View a broadcaster's chat room settings.</summary>
    public const string ModeratorReadChatSettings = "moderator:read:chat_settings";

    /// <summary>Manage a broadcaster's chat room settings.</summary>
    public const string ModeratorManageChatSettings = "moderator:manage:chat_settings";

    /// <summary>View the chatters in a broadcaster's chat room.</summary>
    public const string ModeratorReadChatters = "moderator:read:chatters";

    /// <summary>Read the followers of a broadcaster.</summary>
    public const string ModeratorReadFollowers = "moderator:read:followers";

    /// <summary>Read Guest Star details for channels where you are a moderator.</summary>
    public const string ModeratorReadGuestStar = "moderator:read:guest_star";

    /// <summary>Manage Guest Star for channels where you are a moderator.</summary>
    public const string ModeratorManageGuestStar = "moderator:manage:guest_star";

    /// <summary>Read the list of moderators where you have the moderator role.</summary>
    public const string ModeratorReadModerators = "moderator:read:moderators";

    /// <summary>View a broadcaster's Shield Mode status.</summary>
    public const string ModeratorReadShieldMode = "moderator:read:shield_mode";

    /// <summary>Manage a broadcaster's Shield Mode status.</summary>
    public const string ModeratorManageShieldMode = "moderator:manage:shield_mode";

    /// <summary>View a broadcaster's shoutouts.</summary>
    public const string ModeratorReadShoutouts = "moderator:read:shoutouts";

    /// <summary>Manage a broadcaster's shoutouts.</summary>
    public const string ModeratorManageShoutouts = "moderator:manage:shoutouts";

    /// <summary>Read chat from suspicious users and see flagged users.</summary>
    public const string ModeratorReadSuspiciousUsers = "moderator:read:suspicious_users";

    /// <summary>Manage suspicious user statuses where you have the moderator role.</summary>
    public const string ModeratorManageSuspiciousUsers = "moderator:manage:suspicious_users";

    /// <summary>View a broadcaster's unban requests.</summary>
    public const string ModeratorReadUnbanRequests = "moderator:read:unban_requests";

    /// <summary>Manage a broadcaster's unban requests.</summary>
    public const string ModeratorManageUnbanRequests = "moderator:manage:unban_requests";

    /// <summary>Read the list of VIPs where you have the moderator role.</summary>
    public const string ModeratorReadVips = "moderator:read:vips";

    /// <summary>Read warnings where you have the moderator role.</summary>
    public const string ModeratorReadWarnings = "moderator:read:warnings";

    /// <summary>Warn users where you have the moderator role.</summary>
    public const string ModeratorManageWarnings = "moderator:manage:warnings";

    // User

    /// <summary>Join chat channel as your user and appear as a bot.</summary>
    public const string UserBot = "user:bot";

    /// <summary>Manage a user object.</summary>
    public const string UserEdit = "user:edit";

    /// <summary>View and edit a user's broadcasting configuration.</summary>
    public const string UserEditBroadcast = "user:edit:broadcast";

    /// <summary>View the block list of a user.</summary>
    public const string UserReadBlockedUsers = "user:read:blocked_users";

    /// <summary>Manage the block list of a user.</summary>
    public const string UserManageBlockedUsers = "user:manage:blocked_users";

    /// <summary>View a user's broadcasting configuration.</summary>
    public const string UserReadBroadcast = "user:read:broadcast";

    /// <summary>Receive chatroom messages and informational notifications.</summary>
    public const string UserReadChat = "user:read:chat";

    /// <summary>Update the color used for the user's name in chat.</summary>
    public const string UserManageChatColor = "user:manage:chat_color";

    /// <summary>View a user's email address.</summary>
    public const string UserReadEmail = "user:read:email";

    /// <summary>View emotes available to a user.</summary>
    public const string UserReadEmotes = "user:read:emotes";

    /// <summary>View the list of channels a user follows.</summary>
    public const string UserReadFollows = "user:read:follows";

    /// <summary>Read channels you have moderator privileges in.</summary>
    public const string UserReadModeratedChannels = "user:read:moderated_channels";

    /// <summary>View if authorized user is subscribed to specific channels.</summary>
    public const string UserReadSubscriptions = "user:read:subscriptions";

    /// <summary>Receive whispers sent to your user.</summary>
    public const string UserReadWhispers = "user:read:whispers";

    /// <summary>Receive and send whispers on your user's behalf.</summary>
    public const string UserManageWhispers = "user:manage:whispers";

    /// <summary>Send chat messages to a chatroom.</summary>
    public const string UserWriteChat = "user:write:chat";

    // IRC Chat

    /// <summary>Send chat messages to a chatroom using an IRC connection.</summary>
    public const string ChatEdit = "chat:edit";

    /// <summary>View chat messages sent in a chatroom using an IRC connection.</summary>
    public const string ChatRead = "chat:read";

    // Legacy (PubSub-era scopes — PubSub was deprecated April 2025, but these scopes remain valid)

    /// <summary>Receive whisper messages for your user.</summary>
    public const string WhispersRead = "whispers:read";
}
