using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Internal response wrapper for the Update Conduit Shards endpoint,
/// which returns both successful updates and errors.
/// </summary>
internal sealed record UpdateConduitShardsResponse
{
    [JsonPropertyName("data")]
    public ConduitShardData[]? Data { get; init; }

    [JsonPropertyName("errors")]
    public ConduitShardErrorData[]? Errors { get; init; }
}
