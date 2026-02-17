namespace TwitchSharp.Api.Clients;

/// <summary>
/// Result of updating conduit shards, containing both successful updates and errors.
/// </summary>
public sealed record UpdateConduitShardsResult
{
    /// <summary>The shards that were successfully updated.</summary>
    public ConduitShardData[] Data { get; init; } = [];

    /// <summary>The shards that failed to update.</summary>
    public ConduitShardErrorData[] Errors { get; init; } = [];
}
