using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents prime paid upgrade data for a chat notification.</summary>
public sealed record ChatNotificationPrimePaidUpgradeData
{
    /// <summary>The subscription tier.</summary>
    [JsonPropertyName("sub_tier")]
    public string SubTier { get; init; } = string.Empty;
}
