using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents an error that occurred while updating a conduit shard.
/// </summary>
public sealed record ConduitShardErrorData
{
    /// <summary>The shard ID.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The error message describing what went wrong.</summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    /// <summary>The error code representing the specific error condition.</summary>
    [JsonPropertyName("code")]
    public string Code { get; init; } = string.Empty;
}
