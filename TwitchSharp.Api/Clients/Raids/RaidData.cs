using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the data returned when starting a raid.
/// </summary>
public sealed record RaidData
{
    /// <summary>The UTC date and time (in RFC3339 format) of when the raid was requested.</summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = string.Empty;

    /// <summary>Whether the channel being raided contains mature content. Deprecated: always returns false.</summary>
    [JsonPropertyName("is_mature")]
    public bool IsMature { get; init; }
}
