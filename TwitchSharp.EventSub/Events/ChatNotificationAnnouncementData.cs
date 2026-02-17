using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents announcement data for a chat notification.</summary>
public sealed record ChatNotificationAnnouncementData
{
    /// <summary>The color of the announcement.</summary>
    [JsonPropertyName("color")]
    public string Color { get; init; } = string.Empty;
}
