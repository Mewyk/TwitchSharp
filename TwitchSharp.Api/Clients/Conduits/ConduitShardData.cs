using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a shard within a conduit.
/// </summary>
public sealed record ConduitShardData
{
    /// <summary>The shard ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The shard status. The subscriber receives events only for enabled shards.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>The transport details used to send notifications.</summary>
    [JsonPropertyName("transport")]
    public EventSubTransportData Transport { get; init; } = new();
}
