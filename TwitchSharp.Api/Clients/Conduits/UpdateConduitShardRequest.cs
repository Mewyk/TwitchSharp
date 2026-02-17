using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a single shard update within an Update Conduit Shards request.
/// </summary>
public sealed record UpdateConduitShardRequest
{
    /// <summary>The shard ID. Required.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The transport details for this shard. Required.</summary>
    [JsonPropertyName("transport")]
    public UpdateConduitShardTransportRequest Transport { get; init; } = new();
}
