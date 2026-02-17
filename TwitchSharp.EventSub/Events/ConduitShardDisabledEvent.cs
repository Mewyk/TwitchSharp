using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a conduit.shard.disabled v1 event, fired when a conduit shard is disabled.</summary>
public sealed record ConduitShardDisabledEvent
{
    /// <summary>The conduit ID.</summary>
    [JsonPropertyName("conduit_id")]
    public string ConduitId { get; init; } = string.Empty;

    /// <summary>The shard ID that was disabled.</summary>
    [JsonPropertyName("shard_id")]
    public string ShardId { get; init; } = string.Empty;

    /// <summary>The status of the shard.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>The transport configuration of the disabled shard.</summary>
    [JsonPropertyName("transport")]
    public ConduitShardTransportData Transport { get; init; } = new();

    /// <summary>The UTC timestamp of when the shard was disabled.</summary>
    [JsonPropertyName("disabled_at")]
    public string DisabledAt { get; init; } = string.Empty;
}
