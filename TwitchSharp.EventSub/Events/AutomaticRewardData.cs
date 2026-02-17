using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents the automatic reward details for a channel points automatic
/// reward redemption event.
/// </summary>
public sealed record AutomaticRewardData
{
    /// <summary>The type of the automatic reward (for example, single_message_bypass_sub_mode, send_highlighted_message, chosen_modified_sub_emote_unlock, etc.).</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The cost of the reward in channel points.</summary>
    [JsonPropertyName("channel_points")]
    public int ChannelPoints { get; init; }

    /// <summary>The emote associated with the reward, or null if the reward type does not involve an emote.</summary>
    [JsonPropertyName("emote")]
    public UnlockedEmoteData? Emote { get; init; }
}
