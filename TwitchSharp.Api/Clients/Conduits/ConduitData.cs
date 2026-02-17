using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a conduit for EventSub event routing.
/// </summary>
public sealed record ConduitData
{
    /// <summary>The conduit ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The number of shards associated with this conduit.</summary>
    [JsonPropertyName("shard_count")]
    public int ShardCount { get; init; }
}
