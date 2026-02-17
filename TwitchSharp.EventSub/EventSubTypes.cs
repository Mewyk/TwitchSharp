namespace TwitchSharp.EventSub;

/// <summary>
/// String constants for all Twitch EventSub subscription type names.
/// </summary>
public static class EventSubTypes
{
    // ── Channel Events ──────────────────────────────────────────────────

    /// <summary>A broadcaster updates their channel properties.</summary>
    public const string ChannelUpdate = "channel.update";

    /// <summary>A specified channel receives a follow.</summary>
    public const string ChannelFollow = "channel.follow";

    /// <summary>A notification when a specified channel receives a subscriber.</summary>
    public const string ChannelSubscribe = "channel.subscribe";

    /// <summary>A notification when a subscription to the specified channel ends.</summary>
    public const string ChannelSubscriptionEnd = "channel.subscription.end";

    /// <summary>A notification when a viewer gives a gift subscription in the specified channel.</summary>
    public const string ChannelSubscriptionGift = "channel.subscription.gift";

    /// <summary>A notification when a user sends a resubscription chat message in the specified channel.</summary>
    public const string ChannelSubscriptionMessage = "channel.subscription.message";

    /// <summary>A user cheers on the specified channel.</summary>
    public const string ChannelCheer = "channel.cheer";

    /// <summary>A broadcaster raids another broadcaster's channel.</summary>
    public const string ChannelRaid = "channel.raid";

    /// <summary>A viewer is banned from the specified channel.</summary>
    public const string ChannelBan = "channel.ban";

    /// <summary>A viewer is unbanned from the specified channel.</summary>
    public const string ChannelUnban = "channel.unban";

    /// <summary>Moderator privileges were added to a user on the specified channel.</summary>
    public const string ChannelModeratorAdd = "channel.moderator.add";

    /// <summary>Moderator privileges were removed from a user on the specified channel.</summary>
    public const string ChannelModeratorRemove = "channel.moderator.remove";

    /// <summary>An ad break has begun on the specified channel.</summary>
    public const string ChannelAdBreakBegin = "channel.ad_break.begin";

    /// <summary>Chat is cleared on the specified channel.</summary>
    public const string ChannelChatClear = "channel.chat.clear";

    /// <summary>All messages from a specific user are removed from the specified channel.</summary>
    public const string ChannelChatClearUserMessages = "channel.chat.clear_user_messages";

    /// <summary>A chat message is sent in the specified channel.</summary>
    public const string ChannelChatMessage = "channel.chat.message";

    /// <summary>A chat message is deleted in the specified channel.</summary>
    public const string ChannelChatMessageDelete = "channel.chat.message_delete";

    /// <summary>A notification is sent to the specified channel's chat.</summary>
    public const string ChannelChatNotification = "channel.chat.notification";

    /// <summary>The chat settings for the specified channel are updated.</summary>
    public const string ChannelChatSettingsUpdate = "channel.chat_settings.update";

    /// <summary>A custom channel points reward is added to the specified channel.</summary>
    public const string ChannelPointsCustomRewardAdd = "channel.channel_points_custom_reward.add";

    /// <summary>A custom channel points reward is updated on the specified channel.</summary>
    public const string ChannelPointsCustomRewardUpdate = "channel.channel_points_custom_reward.update";

    /// <summary>A custom channel points reward is removed from the specified channel.</summary>
    public const string ChannelPointsCustomRewardRemove = "channel.channel_points_custom_reward.remove";

    /// <summary>A viewer redeems a custom channel points reward on the specified channel.</summary>
    public const string ChannelPointsCustomRewardRedemptionAdd = "channel.channel_points_custom_reward_redemption.add";

    /// <summary>A redemption of a custom channel points reward is updated on the specified channel.</summary>
    public const string ChannelPointsCustomRewardRedemptionUpdate = "channel.channel_points_custom_reward_redemption.update";

    /// <summary>A viewer redeems an automatic channel points reward on the specified channel.</summary>
    public const string ChannelPointsAutomaticRewardRedemptionAdd = "channel.channel_points_automatic_reward_redemption.add";

    /// <summary>A poll begins on the specified channel.</summary>
    public const string ChannelPollBegin = "channel.poll.begin";

    /// <summary>Users respond to a poll on the specified channel.</summary>
    public const string ChannelPollProgress = "channel.poll.progress";

    /// <summary>A poll ends on the specified channel.</summary>
    public const string ChannelPollEnd = "channel.poll.end";

    /// <summary>A prediction begins on the specified channel.</summary>
    public const string ChannelPredictionBegin = "channel.prediction.begin";

    /// <summary>Users participate in a prediction on the specified channel.</summary>
    public const string ChannelPredictionProgress = "channel.prediction.progress";

    /// <summary>A prediction is locked on the specified channel.</summary>
    public const string ChannelPredictionLock = "channel.prediction.lock";

    /// <summary>A prediction ends on the specified channel.</summary>
    public const string ChannelPredictionEnd = "channel.prediction.end";

    /// <summary>A user donates to the broadcaster's charity campaign.</summary>
    public const string ChannelCharityCampaignDonate = "channel.charity_campaign.donate";

    /// <summary>A charity campaign starts on the specified channel.</summary>
    public const string ChannelCharityCampaignStart = "channel.charity_campaign.start";

    /// <summary>Progress is made towards the campaign's goal on the specified channel.</summary>
    public const string ChannelCharityCampaignProgress = "channel.charity_campaign.progress";

    /// <summary>A charity campaign stops on the specified channel.</summary>
    public const string ChannelCharityCampaignStop = "channel.charity_campaign.stop";

    /// <summary>A goal begins on the specified channel.</summary>
    public const string ChannelGoalBegin = "channel.goal.begin";

    /// <summary>Progress is made towards a goal on the specified channel.</summary>
    public const string ChannelGoalProgress = "channel.goal.progress";

    /// <summary>A goal ends on the specified channel.</summary>
    public const string ChannelGoalEnd = "channel.goal.end";

    /// <summary>A Hype Train begins on the specified channel.</summary>
    public const string ChannelHypeTrainBegin = "channel.hype_train.begin";

    /// <summary>A Hype Train makes progress on the specified channel.</summary>
    public const string ChannelHypeTrainProgress = "channel.hype_train.progress";

    /// <summary>A Hype Train ends on the specified channel.</summary>
    public const string ChannelHypeTrainEnd = "channel.hype_train.end";

    /// <summary>Shield Mode is activated on the specified channel.</summary>
    public const string ChannelShieldModeBegin = "channel.shield_mode.begin";

    /// <summary>Shield Mode is deactivated on the specified channel.</summary>
    public const string ChannelShieldModeEnd = "channel.shield_mode.end";

    /// <summary>A shoutout is created on the specified channel.</summary>
    public const string ChannelShoutoutCreate = "channel.shoutout.create";

    /// <summary>A shoutout is received by the specified channel.</summary>
    public const string ChannelShoutoutReceive = "channel.shoutout.receive";

    /// <summary>A suspicious user sends a message in the specified channel.</summary>
    public const string ChannelSuspiciousUserMessage = "channel.suspicious_user.message";

    /// <summary>A suspicious user is updated in the specified channel.</summary>
    public const string ChannelSuspiciousUserUpdate = "channel.suspicious_user.update";

    /// <summary>A VIP is added to the specified channel.</summary>
    public const string ChannelVipAdd = "channel.vip.add";

    /// <summary>A VIP is removed from the specified channel.</summary>
    public const string ChannelVipRemove = "channel.vip.remove";

    /// <summary>A warning is acknowledged by a user in the specified channel.</summary>
    public const string ChannelWarningAcknowledge = "channel.warning.acknowledge";

    /// <summary>A warning is sent to a user in the specified channel.</summary>
    public const string ChannelWarningSend = "channel.warning.send";

    /// <summary>An unban request is created in the specified channel.</summary>
    public const string ChannelUnbanRequestCreate = "channel.unban_request.create";

    /// <summary>An unban request is resolved in the specified channel.</summary>
    public const string ChannelUnbanRequestResolve = "channel.unban_request.resolve";

    /// <summary>A moderator performs a moderation action in the specified channel.</summary>
    public const string ChannelModerate = "channel.moderate";

    /// <summary>A user uses Bits in the specified channel.</summary>
    public const string ChannelBitsUse = "channel.bits.use";

    /// <summary>A user's message is held by chat settings in the specified channel.</summary>
    public const string ChannelChatUserMessageHold = "channel.chat.user_message_hold";

    /// <summary>A user's held message is updated in the specified channel.</summary>
    public const string ChannelChatUserMessageUpdate = "channel.chat.user_message_update";

    // ── Shared Chat Events ─────────────────────────────────────────────

    /// <summary>A shared chat session begins on the specified channel.</summary>
    public const string ChannelSharedChatBegin = "channel.shared_chat.begin";

    /// <summary>A shared chat session is updated on the specified channel.</summary>
    public const string ChannelSharedChatUpdate = "channel.shared_chat.update";

    /// <summary>A shared chat session ends on the specified channel.</summary>
    public const string ChannelSharedChatEnd = "channel.shared_chat.end";

    // ── Stream Events ───────────────────────────────────────────────────

    /// <summary>The specified broadcaster starts a stream.</summary>
    public const string StreamOnline = "stream.online";

    /// <summary>The specified broadcaster stops a stream.</summary>
    public const string StreamOffline = "stream.offline";

    // ── User Events ─────────────────────────────────────────────────────

    /// <summary>A user's authorization has been granted to a client.</summary>
    public const string UserAuthorizationGrant = "user.authorization.grant";

    /// <summary>A user's authorization has been revoked for a client.</summary>
    public const string UserAuthorizationRevoke = "user.authorization.revoke";

    /// <summary>A user has updated their account.</summary>
    public const string UserUpdate = "user.update";

    /// <summary>A user receives a whisper.</summary>
    public const string UserWhisperMessage = "user.whisper.message";

    // ── Conduit Events ──────────────────────────────────────────────────

    /// <summary>A conduit shard is disabled.</summary>
    public const string ConduitShardDisabled = "conduit.shard.disabled";

    // ── Guest Star Events (BETA) ────────────────────────────────────────

    /// <summary>A Guest Star session begins on the specified channel. This type is in beta.</summary>
    public const string GuestStarSessionBegin = "channel.guest_star_session.begin";

    /// <summary>A Guest Star session ends on the specified channel. This type is in beta.</summary>
    public const string GuestStarSessionEnd = "channel.guest_star_session.end";

    /// <summary>A Guest Star guest is updated on the specified channel. This type is in beta.</summary>
    public const string GuestStarGuestUpdate = "channel.guest_star_guest.update";

    /// <summary>Guest Star settings are updated on the specified channel. This type is in beta.</summary>
    public const string GuestStarSettingsUpdate = "channel.guest_star_settings.update";

    // ── Automod Events ──────────────────────────────────────────────────

    /// <summary>A message is held by AutoMod for review.</summary>
    public const string AutomodMessageHold = "automod.message.hold";

    /// <summary>A held AutoMod message is updated.</summary>
    public const string AutomodMessageUpdate = "automod.message.update";

    /// <summary>AutoMod settings are updated on the specified channel.</summary>
    public const string AutomodSettingsUpdate = "automod.settings.update";

    /// <summary>AutoMod terms are updated on the specified channel.</summary>
    public const string AutomodTermsUpdate = "automod.terms.update";

    // ── Webhook-Only Events ─────────────────────────────────────────────

    /// <summary>A Drop entitlement is granted. This type is webhook-only and cannot be used with WebSocket.</summary>
    public const string DropEntitlementGrant = "drop.entitlement.grant";

    /// <summary>An extension Bits transaction is created. This type is webhook-only and cannot be used with WebSocket.</summary>
    public const string ExtensionBitsTransactionCreate = "extension.bits_transaction.create";

    /// <summary>
    /// Subscription version constants corresponding to each EventSub type.
    /// </summary>
    public static class Versions
    {
        // ── Channel Events ──────────────────────────────────────────────

        /// <summary>Version for <see cref="EventSubTypes.ChannelUpdate"/>.</summary>
        public const string ChannelUpdate = "2";

        /// <summary>Version for <see cref="EventSubTypes.ChannelFollow"/>.</summary>
        public const string ChannelFollow = "2";

        /// <summary>Version for <see cref="EventSubTypes.ChannelSubscribe"/>.</summary>
        public const string ChannelSubscribe = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelSubscriptionEnd"/>.</summary>
        public const string ChannelSubscriptionEnd = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelSubscriptionGift"/>.</summary>
        public const string ChannelSubscriptionGift = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelSubscriptionMessage"/>.</summary>
        public const string ChannelSubscriptionMessage = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelCheer"/>.</summary>
        public const string ChannelCheer = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelRaid"/>.</summary>
        public const string ChannelRaid = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelBan"/>.</summary>
        public const string ChannelBan = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelUnban"/>.</summary>
        public const string ChannelUnban = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelModeratorAdd"/>.</summary>
        public const string ChannelModeratorAdd = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelModeratorRemove"/>.</summary>
        public const string ChannelModeratorRemove = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelAdBreakBegin"/>.</summary>
        public const string ChannelAdBreakBegin = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelChatClear"/>.</summary>
        public const string ChannelChatClear = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelChatClearUserMessages"/>.</summary>
        public const string ChannelChatClearUserMessages = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelChatMessage"/>.</summary>
        public const string ChannelChatMessage = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelChatMessageDelete"/>.</summary>
        public const string ChannelChatMessageDelete = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelChatNotification"/>.</summary>
        public const string ChannelChatNotification = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelChatSettingsUpdate"/>.</summary>
        public const string ChannelChatSettingsUpdate = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelPointsCustomRewardAdd"/>.</summary>
        public const string ChannelPointsCustomRewardAdd = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelPointsCustomRewardUpdate"/>.</summary>
        public const string ChannelPointsCustomRewardUpdate = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelPointsCustomRewardRemove"/>.</summary>
        public const string ChannelPointsCustomRewardRemove = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelPointsCustomRewardRedemptionAdd"/>.</summary>
        public const string ChannelPointsCustomRewardRedemptionAdd = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelPointsCustomRewardRedemptionUpdate"/>.</summary>
        public const string ChannelPointsCustomRewardRedemptionUpdate = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelPointsAutomaticRewardRedemptionAdd"/>.</summary>
        public const string ChannelPointsAutomaticRewardRedemptionAdd = "2";

        /// <summary>Version for <see cref="EventSubTypes.ChannelPollBegin"/>.</summary>
        public const string ChannelPollBegin = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelPollProgress"/>.</summary>
        public const string ChannelPollProgress = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelPollEnd"/>.</summary>
        public const string ChannelPollEnd = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelPredictionBegin"/>.</summary>
        public const string ChannelPredictionBegin = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelPredictionProgress"/>.</summary>
        public const string ChannelPredictionProgress = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelPredictionLock"/>.</summary>
        public const string ChannelPredictionLock = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelPredictionEnd"/>.</summary>
        public const string ChannelPredictionEnd = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelCharityCampaignDonate"/>.</summary>
        public const string ChannelCharityCampaignDonate = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelCharityCampaignStart"/>.</summary>
        public const string ChannelCharityCampaignStart = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelCharityCampaignProgress"/>.</summary>
        public const string ChannelCharityCampaignProgress = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelCharityCampaignStop"/>.</summary>
        public const string ChannelCharityCampaignStop = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelGoalBegin"/>.</summary>
        public const string ChannelGoalBegin = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelGoalProgress"/>.</summary>
        public const string ChannelGoalProgress = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelGoalEnd"/>.</summary>
        public const string ChannelGoalEnd = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelHypeTrainBegin"/>.</summary>
        public const string ChannelHypeTrainBegin = "2";

        /// <summary>Version for <see cref="EventSubTypes.ChannelHypeTrainProgress"/>.</summary>
        public const string ChannelHypeTrainProgress = "2";

        /// <summary>Version for <see cref="EventSubTypes.ChannelHypeTrainEnd"/>.</summary>
        public const string ChannelHypeTrainEnd = "2";

        /// <summary>Version for <see cref="EventSubTypes.ChannelShieldModeBegin"/>.</summary>
        public const string ChannelShieldModeBegin = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelShieldModeEnd"/>.</summary>
        public const string ChannelShieldModeEnd = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelShoutoutCreate"/>.</summary>
        public const string ChannelShoutoutCreate = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelShoutoutReceive"/>.</summary>
        public const string ChannelShoutoutReceive = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelSuspiciousUserMessage"/>.</summary>
        public const string ChannelSuspiciousUserMessage = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelSuspiciousUserUpdate"/>.</summary>
        public const string ChannelSuspiciousUserUpdate = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelVipAdd"/>.</summary>
        public const string ChannelVipAdd = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelVipRemove"/>.</summary>
        public const string ChannelVipRemove = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelWarningAcknowledge"/>.</summary>
        public const string ChannelWarningAcknowledge = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelWarningSend"/>.</summary>
        public const string ChannelWarningSend = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelUnbanRequestCreate"/>.</summary>
        public const string ChannelUnbanRequestCreate = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelUnbanRequestResolve"/>.</summary>
        public const string ChannelUnbanRequestResolve = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelModerate"/>.</summary>
        public const string ChannelModerate = "2";

        /// <summary>Version for <see cref="EventSubTypes.ChannelBitsUse"/>.</summary>
        public const string ChannelBitsUse = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelChatUserMessageHold"/>.</summary>
        public const string ChannelChatUserMessageHold = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelChatUserMessageUpdate"/>.</summary>
        public const string ChannelChatUserMessageUpdate = "1";

        // ── Shared Chat Events ─────────────────────────────────────────

        /// <summary>Version for <see cref="EventSubTypes.ChannelSharedChatBegin"/>.</summary>
        public const string ChannelSharedChatBegin = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelSharedChatUpdate"/>.</summary>
        public const string ChannelSharedChatUpdate = "1";

        /// <summary>Version for <see cref="EventSubTypes.ChannelSharedChatEnd"/>.</summary>
        public const string ChannelSharedChatEnd = "1";

        // ── Stream Events ───────────────────────────────────────────────

        /// <summary>Version for <see cref="EventSubTypes.StreamOnline"/>.</summary>
        public const string StreamOnline = "1";

        /// <summary>Version for <see cref="EventSubTypes.StreamOffline"/>.</summary>
        public const string StreamOffline = "1";

        // ── User Events ─────────────────────────────────────────────────

        /// <summary>Version for <see cref="EventSubTypes.UserAuthorizationGrant"/>.</summary>
        public const string UserAuthorizationGrant = "1";

        /// <summary>Version for <see cref="EventSubTypes.UserAuthorizationRevoke"/>.</summary>
        public const string UserAuthorizationRevoke = "1";

        /// <summary>Version for <see cref="EventSubTypes.UserUpdate"/>.</summary>
        public const string UserUpdate = "1";

        /// <summary>Version for <see cref="EventSubTypes.UserWhisperMessage"/>.</summary>
        public const string UserWhisperMessage = "1";

        // ── Conduit Events ──────────────────────────────────────────────

        /// <summary>Version for <see cref="EventSubTypes.ConduitShardDisabled"/>.</summary>
        public const string ConduitShardDisabled = "1";

        // ── Guest Star Events (BETA) ────────────────────────────────────

        /// <summary>Version for <see cref="EventSubTypes.GuestStarSessionBegin"/>.</summary>
        public const string GuestStarSessionBegin = "beta";

        /// <summary>Version for <see cref="EventSubTypes.GuestStarSessionEnd"/>.</summary>
        public const string GuestStarSessionEnd = "beta";

        /// <summary>Version for <see cref="EventSubTypes.GuestStarGuestUpdate"/>.</summary>
        public const string GuestStarGuestUpdate = "beta";

        /// <summary>Version for <see cref="EventSubTypes.GuestStarSettingsUpdate"/>.</summary>
        public const string GuestStarSettingsUpdate = "beta";

        // ── Automod Events ──────────────────────────────────────────────

        /// <summary>Version for <see cref="EventSubTypes.AutomodMessageHold"/>.</summary>
        public const string AutomodMessageHold = "2";

        /// <summary>Version for <see cref="EventSubTypes.AutomodMessageUpdate"/>.</summary>
        public const string AutomodMessageUpdate = "2";

        /// <summary>Version for <see cref="EventSubTypes.AutomodSettingsUpdate"/>.</summary>
        public const string AutomodSettingsUpdate = "1";

        /// <summary>Version for <see cref="EventSubTypes.AutomodTermsUpdate"/>.</summary>
        public const string AutomodTermsUpdate = "1";

        // ── Webhook-Only Events ─────────────────────────────────────────

        /// <summary>Version for <see cref="EventSubTypes.DropEntitlementGrant"/>.</summary>
        public const string DropEntitlementGrant = "1";

        /// <summary>Version for <see cref="EventSubTypes.ExtensionBitsTransactionCreate"/>.</summary>
        public const string ExtensionBitsTransactionCreate = "1";
    }
}
