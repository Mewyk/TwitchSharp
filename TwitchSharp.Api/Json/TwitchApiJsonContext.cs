using System.Text.Json.Serialization;
using TwitchSharp.Api.Authentication;
using TwitchSharp.Api.Clients;
using TwitchSharp.Api.Http;

namespace TwitchSharp.Api.Json;

/// <summary>
/// Source-generated JSON serializer context for TwitchSharp.Api types.
/// All serializable types must be registered here in a single file
/// to avoid .NET source generator hintName collisions.
/// </summary>
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    UseStringEnumConverter = true)]
// Infrastructure
[JsonSerializable(typeof(OAuthTokenResponse))]
[JsonSerializable(typeof(TwitchErrorResponse))]
[JsonSerializable(typeof(TokenValidationData))]
[JsonSerializable(typeof(DeviceCodeData))]
// Users
[JsonSerializable(typeof(HelixDataResponse<UserData>))]
[JsonSerializable(typeof(HelixDataResponse<UserBlockData>))]
[JsonSerializable(typeof(HelixDataResponse<UserExtensionData>))]
[JsonSerializable(typeof(ActiveExtensionsResponse))]
[JsonSerializable(typeof(ActiveExtensionsPayload))]
// Channels
[JsonSerializable(typeof(HelixDataResponse<ChannelInformationData>))]
[JsonSerializable(typeof(ModifyChannelInformationRequest))]
[JsonSerializable(typeof(HelixDataResponse<ChannelEditorData>))]
[JsonSerializable(typeof(HelixDataResponse<FollowedChannelData>))]
[JsonSerializable(typeof(HelixDataResponse<ChannelFollowerData>))]
// Streams
[JsonSerializable(typeof(HelixDataResponse<StreamData>))]
[JsonSerializable(typeof(HelixDataResponse<StreamKeyData>))]
[JsonSerializable(typeof(CreateStreamMarkerRequest))]
[JsonSerializable(typeof(HelixDataResponse<StreamMarkerData>))]
[JsonSerializable(typeof(HelixDataResponse<StreamMarkerContainerData>))]
// Games
[JsonSerializable(typeof(HelixDataResponse<GameData>))]
// Search
[JsonSerializable(typeof(HelixDataResponse<SearchChannelData>))]
// Chat
[JsonSerializable(typeof(HelixDataResponse<ChatterData>))]
[JsonSerializable(typeof(HelixDataResponse<EmoteData>))]
[JsonSerializable(typeof(HelixDataResponse<BadgeSetData>))]
[JsonSerializable(typeof(HelixDataResponse<ChatSettingsData>))]
[JsonSerializable(typeof(UpdateChatSettingsRequest))]
[JsonSerializable(typeof(HelixDataResponse<ChatColorData>))]
[JsonSerializable(typeof(SendAnnouncementRequest))]
[JsonSerializable(typeof(SendMessageRequest))]
[JsonSerializable(typeof(HelixDataResponse<SendMessageResponseData>))]
[JsonSerializable(typeof(HelixDataResponse<SharedChatSessionData>))]
// Goals
[JsonSerializable(typeof(HelixDataResponse<GoalData>))]
// Hype Train
[JsonSerializable(typeof(HelixDataResponse<HypeTrainStatusData>))]
// Whispers
[JsonSerializable(typeof(SendWhisperRequest))]
// Raids
[JsonSerializable(typeof(HelixDataResponse<RaidData>))]
// Teams
[JsonSerializable(typeof(HelixDataResponse<ChannelTeamData>))]
[JsonSerializable(typeof(HelixDataResponse<TeamData>))]
// Bits
[JsonSerializable(typeof(HelixDataResponse<BitsLeaderboardEntryData>))]
[JsonSerializable(typeof(HelixDataResponse<CheermoteData>))]
[JsonSerializable(typeof(HelixDataResponse<ExtensionTransactionData>))]
// Ads
[JsonSerializable(typeof(StartCommercialRequest))]
[JsonSerializable(typeof(HelixDataResponse<CommercialData>))]
[JsonSerializable(typeof(HelixDataResponse<AdScheduleData>))]
[JsonSerializable(typeof(HelixDataResponse<AdSnoozeData>))]
// Videos
[JsonSerializable(typeof(HelixDataResponse<VideoData>))]
[JsonSerializable(typeof(HelixDataResponse<string>))]
// Clips
[JsonSerializable(typeof(HelixDataResponse<CreateClipData>))]
[JsonSerializable(typeof(HelixDataResponse<ClipData>))]
[JsonSerializable(typeof(HelixDataResponse<ClipDownloadData>))]
// Polls
[JsonSerializable(typeof(HelixDataResponse<PollData>))]
[JsonSerializable(typeof(CreatePollRequest))]
[JsonSerializable(typeof(EndPollRequest))]
// Predictions
[JsonSerializable(typeof(HelixDataResponse<PredictionData>))]
[JsonSerializable(typeof(CreatePredictionRequest))]
[JsonSerializable(typeof(EndPredictionRequest))]
// Schedule
[JsonSerializable(typeof(ScheduleResponse))]
[JsonSerializable(typeof(CreateScheduleSegmentRequest))]
[JsonSerializable(typeof(UpdateScheduleSegmentRequest))]
// Channel Points
[JsonSerializable(typeof(HelixDataResponse<CustomRewardData>))]
[JsonSerializable(typeof(HelixDataResponse<RedemptionData>))]
[JsonSerializable(typeof(CreateCustomRewardRequest))]
[JsonSerializable(typeof(UpdateCustomRewardRequest))]
[JsonSerializable(typeof(UpdateRedemptionStatusRequest))]
// Moderation
[JsonSerializable(typeof(HelixDataResponse<AutoModStatusData>))]
[JsonSerializable(typeof(HelixDataResponse<AutoModSettingsData>))]
[JsonSerializable(typeof(HelixDataResponse<BannedUserData>))]
[JsonSerializable(typeof(HelixDataResponse<BanResponseData>))]
[JsonSerializable(typeof(HelixDataResponse<UnbanRequestData>))]
[JsonSerializable(typeof(HelixDataResponse<BlockedTermData>))]
[JsonSerializable(typeof(HelixDataResponse<ModeratedChannelData>))]
[JsonSerializable(typeof(HelixDataResponse<ModeratorData>))]
[JsonSerializable(typeof(HelixDataResponse<VipData>))]
[JsonSerializable(typeof(HelixDataResponse<ShieldModeData>))]
[JsonSerializable(typeof(HelixDataResponse<WarnResponseData>))]
[JsonSerializable(typeof(HelixDataResponse<SuspiciousUserData>))]
[JsonSerializable(typeof(CheckAutoModStatusRequest))]
[JsonSerializable(typeof(ManageAutoModMessageRequest))]
[JsonSerializable(typeof(UpdateAutoModSettingsRequest))]
[JsonSerializable(typeof(BanUserRequest))]
[JsonSerializable(typeof(AddBlockedTermRequest))]
[JsonSerializable(typeof(UpdateShieldModeRequest))]
[JsonSerializable(typeof(WarnChatUserRequest))]
[JsonSerializable(typeof(AddSuspiciousStatusRequest))]
// Content Classification Labels
[JsonSerializable(typeof(HelixDataResponse<ContentClassificationLabelData>))]
// Analytics
[JsonSerializable(typeof(HelixDataResponse<ExtensionAnalyticsData>))]
[JsonSerializable(typeof(HelixDataResponse<GameAnalyticsData>))]
// Charity
[JsonSerializable(typeof(HelixDataResponse<CharityCampaignData>))]
[JsonSerializable(typeof(HelixDataResponse<CharityDonationData>))]
// Entitlements
[JsonSerializable(typeof(HelixDataResponse<DropsEntitlementData>))]
[JsonSerializable(typeof(HelixDataResponse<UpdateDropsEntitlementData>))]
[JsonSerializable(typeof(UpdateDropsEntitlementsRequest))]
// EventSub
[JsonSerializable(typeof(EventSubResponse))]
[JsonSerializable(typeof(CreateEventSubSubscriptionRequest))]
// Conduits
[JsonSerializable(typeof(HelixDataResponse<ConduitData>))]
[JsonSerializable(typeof(HelixDataResponse<ConduitShardData>))]
[JsonSerializable(typeof(UpdateConduitShardsResponse))]
[JsonSerializable(typeof(CreateConduitRequest))]
[JsonSerializable(typeof(UpdateConduitRequest))]
[JsonSerializable(typeof(UpdateConduitShardsRequest))]
// Extensions
[JsonSerializable(typeof(HelixDataResponse<ExtensionData>))]
[JsonSerializable(typeof(ExtensionLiveChannelsResponse))]
[JsonSerializable(typeof(HelixDataResponse<ExtensionBitsProductData>))]
[JsonSerializable(typeof(UpdateExtensionBitsProductRequest))]
[JsonSerializable(typeof(HelixDataResponse<ExtensionConfigurationData>))]
[JsonSerializable(typeof(SetExtensionConfigurationRequest))]
[JsonSerializable(typeof(SetExtensionRequiredConfigurationRequest))]
[JsonSerializable(typeof(SendExtensionPubSubMessageRequest))]
[JsonSerializable(typeof(SendExtensionChatMessageRequest))]
[JsonSerializable(typeof(HelixDataResponse<ExtensionSecretData>))]
// Subscriptions
[JsonSerializable(typeof(HelixDataResponse<SubscriptionData>))]
[JsonSerializable(typeof(HelixDataResponse<UserSubscriptionData>))]
// OIDC
[JsonSerializable(typeof(OidcIdTokenClaims))]
[JsonSerializable(typeof(UserInfoData))]
// Guest Star
[JsonSerializable(typeof(HelixDataResponse<GuestStarSettingsData>))]
[JsonSerializable(typeof(HelixDataResponse<GuestStarSessionData>))]
[JsonSerializable(typeof(HelixDataResponse<GuestStarInviteData>))]
internal partial class TwitchApiJsonContext : JsonSerializerContext;
