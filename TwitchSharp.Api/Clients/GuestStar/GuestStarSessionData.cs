using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a Guest Star session.
/// This API is in BETA and may change without notice.
/// </summary>
public sealed record GuestStarSessionData
{
    /// <summary>The session ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The guests in this session.</summary>
    [JsonPropertyName("guests")]
    public GuestStarGuestData[] Guests { get; init; } = [];
}
