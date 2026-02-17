using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a stream key returned by the Get Stream Key endpoint.
/// </summary>
public sealed record StreamKeyData
{
    /// <summary>The channel's stream key.</summary>
    [JsonPropertyName("stream_key")]
    public string StreamKey { get; init; } = string.Empty;
}
