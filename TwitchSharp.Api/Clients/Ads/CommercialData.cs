using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the result of starting a commercial.
/// </summary>
public sealed record CommercialData
{
    /// <summary>The length of the commercial requested.</summary>
    [JsonPropertyName("length")]
    public int Length { get; init; }

    /// <summary>A message indicating whether Twitch was able to serve an ad.</summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    /// <summary>The number of seconds to wait before running another commercial.</summary>
    [JsonPropertyName("retry_after")]
    public int RetryAfter { get; init; }
}
