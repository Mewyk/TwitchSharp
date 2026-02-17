using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Internal response wrapper for the Get Extension Live Channels endpoint,
/// where pagination is a raw string cursor rather than a pagination object.
/// </summary>
internal sealed record ExtensionLiveChannelsResponse
{
    [JsonPropertyName("data")]
    public ExtensionLiveChannelData[]? Data { get; init; }

    [JsonPropertyName("pagination")]
    public string? Pagination { get; init; }
}
