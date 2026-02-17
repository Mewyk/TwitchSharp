using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a set of chat badges returned by the Channel and Global Chat Badges endpoints.
/// </summary>
public sealed record BadgeSetData
{
    /// <summary>The badge set's ID (e.g., "subscriber", "bits").</summary>
    [JsonPropertyName("set_id")]
    public string SetId { get; init; } = string.Empty;

    /// <summary>The badge versions in this set.</summary>
    [JsonPropertyName("versions")]
    public BadgeVersionData[] Versions { get; init; } = [];
}
