using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Update Conduit Shards endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record UpdateConduitShardsRequest
{
    /// <summary>The conduit ID. Required.</summary>
    [JsonPropertyName("conduit_id")]
    public string ConduitId { get; init; } = string.Empty;

    /// <summary>The list of shards to update. Required.</summary>
    [JsonPropertyName("shards")]
    public UpdateConduitShardRequest[] Shards { get; init; } = [];
}
