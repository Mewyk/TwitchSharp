using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Create Conduits endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record CreateConduitRequest
{
    /// <summary>The number of shards to create for this conduit. Required.</summary>
    [JsonPropertyName("shard_count")]
    public int ShardCount { get; init; }
}
