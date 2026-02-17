using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Source-generated JSON serializer context for EventSub event types.
/// Use this context with <see cref="EventSubNotification.DeserializeEvent{T}"/>
/// for AOT-safe event deserialization.
/// </summary>
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
// Channel Events
[JsonSerializable(typeof(ChannelUpdateEvent))]
[JsonSerializable(typeof(ChannelFollowEvent))]
[JsonSerializable(typeof(ChannelRaidEvent))]
[JsonSerializable(typeof(ChannelCheerEvent))]
[JsonSerializable(typeof(ChannelAdBreakBeginEvent))]
// Subscription Events
[JsonSerializable(typeof(ChannelSubscribeEvent))]
[JsonSerializable(typeof(ChannelSubscriptionEndEvent))]
[JsonSerializable(typeof(ChannelSubscriptionGiftEvent))]
[JsonSerializable(typeof(ChannelSubscriptionMessageEvent))]
// Stream Events
[JsonSerializable(typeof(StreamOnlineEvent))]
[JsonSerializable(typeof(StreamOfflineEvent))]
// Moderation Events
[JsonSerializable(typeof(ChannelBanEvent))]
[JsonSerializable(typeof(ChannelUnbanEvent))]
[JsonSerializable(typeof(ChannelModeratorAddEvent))]
[JsonSerializable(typeof(ChannelModeratorRemoveEvent))]
[JsonSerializable(typeof(ChannelModerateEvent))]
// Channel Points Events
[JsonSerializable(typeof(ChannelPointsRedemptionEvent))]
[JsonSerializable(typeof(ChannelPointsCustomRewardEvent))]
[JsonSerializable(typeof(ChannelPointsAutomaticRewardRedemptionEvent))]
// Poll Events
[JsonSerializable(typeof(PollBeginEvent))]
[JsonSerializable(typeof(PollProgressEvent))]
[JsonSerializable(typeof(PollEndEvent))]
// Prediction Events
[JsonSerializable(typeof(PredictionBeginEvent))]
[JsonSerializable(typeof(PredictionProgressEvent))]
[JsonSerializable(typeof(PredictionLockEvent))]
[JsonSerializable(typeof(PredictionEndEvent))]
// Hype Train Events
[JsonSerializable(typeof(HypeTrainBeginEvent))]
[JsonSerializable(typeof(HypeTrainProgressEvent))]
[JsonSerializable(typeof(HypeTrainEndEvent))]
// Chat Events
[JsonSerializable(typeof(ChatMessageEvent))]
[JsonSerializable(typeof(ChatNotificationEvent))]
[JsonSerializable(typeof(ChatClearEvent))]
[JsonSerializable(typeof(ChatClearUserMessagesEvent))]
[JsonSerializable(typeof(ChatMessageDeleteEvent))]
[JsonSerializable(typeof(ChannelChatSettingsUpdateEvent))]
// Charity Campaign Events
[JsonSerializable(typeof(ChannelCharityCampaignDonateEvent))]
[JsonSerializable(typeof(ChannelCharityCampaignStartEvent))]
[JsonSerializable(typeof(ChannelCharityCampaignProgressEvent))]
[JsonSerializable(typeof(ChannelCharityCampaignStopEvent))]
// Goal Events
[JsonSerializable(typeof(ChannelGoalBeginEvent))]
[JsonSerializable(typeof(ChannelGoalProgressEvent))]
[JsonSerializable(typeof(ChannelGoalEndEvent))]
// Shield Mode Events
[JsonSerializable(typeof(ChannelShieldModeBeginEvent))]
[JsonSerializable(typeof(ChannelShieldModeEndEvent))]
// Shoutout Events
[JsonSerializable(typeof(ChannelShoutoutCreateEvent))]
[JsonSerializable(typeof(ChannelShoutoutReceiveEvent))]
// VIP Events
[JsonSerializable(typeof(ChannelVipAddEvent))]
[JsonSerializable(typeof(ChannelVipRemoveEvent))]
// Warning Events
[JsonSerializable(typeof(ChannelWarningAcknowledgeEvent))]
[JsonSerializable(typeof(ChannelWarningSendEvent))]
// Suspicious User Events
[JsonSerializable(typeof(ChannelSuspiciousUserMessageEvent))]
[JsonSerializable(typeof(ChannelSuspiciousUserUpdateEvent))]
// Unban Request Events
[JsonSerializable(typeof(ChannelUnbanRequestCreateEvent))]
[JsonSerializable(typeof(ChannelUnbanRequestResolveEvent))]
// AutoMod Events
[JsonSerializable(typeof(AutomodMessageHoldEvent))]
[JsonSerializable(typeof(AutomodMessageUpdateEvent))]
[JsonSerializable(typeof(AutomodSettingsUpdateEvent))]
[JsonSerializable(typeof(AutomodTermsUpdateEvent))]
// User Events
[JsonSerializable(typeof(UserAuthorizationGrantEvent))]
[JsonSerializable(typeof(UserAuthorizationRevokeEvent))]
[JsonSerializable(typeof(UserUpdateEvent))]
[JsonSerializable(typeof(UserWhisperMessageEvent))]
// Conduit Events
[JsonSerializable(typeof(ConduitShardDisabledEvent))]
// Shared Chat Events
[JsonSerializable(typeof(SharedChatBeginEvent))]
[JsonSerializable(typeof(SharedChatUpdateEvent))]
[JsonSerializable(typeof(SharedChatEndEvent))]
// Bits Use Events
[JsonSerializable(typeof(ChannelBitsUseEvent))]
// User Message Hold/Update Events
[JsonSerializable(typeof(ChatUserMessageHoldEvent))]
[JsonSerializable(typeof(ChatUserMessageUpdateEvent))]
// Guest Star Events (BETA)
[JsonSerializable(typeof(GuestStarSessionBeginEvent))]
[JsonSerializable(typeof(GuestStarSessionEndEvent))]
[JsonSerializable(typeof(GuestStarGuestUpdateEvent))]
[JsonSerializable(typeof(GuestStarSettingsUpdateEvent))]
// Webhook-Only Events
[JsonSerializable(typeof(DropEntitlementGrantEvent))]
[JsonSerializable(typeof(ExtensionBitsTransactionCreateEvent))]
public partial class EventSubEventsJsonContext : JsonSerializerContext;
