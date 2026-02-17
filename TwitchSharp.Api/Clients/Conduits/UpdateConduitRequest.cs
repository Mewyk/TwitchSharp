using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Update Conduits endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record UpdateConduitRequest
{
    /// <summary>The conduit ID. Required.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The new number of shards for this conduit. Required.</summary>
    [JsonPropertyName("shard_count")]
    public int ShardCount { get; init; }
}
