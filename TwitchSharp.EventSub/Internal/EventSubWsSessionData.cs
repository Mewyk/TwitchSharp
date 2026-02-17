using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Internal;

/// <summary>
/// Internal session data from welcome and reconnect messages.
/// </summary>
internal sealed record EventSubWsSessionData
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("keepalive_timeout_seconds")]
    public int? KeepaliveTimeoutSeconds { get; init; }

    [JsonPropertyName("reconnect_url")]
    public string? ReconnectUrl { get; init; }

    [JsonPropertyName("connected_at")]
    public string ConnectedAt { get; init; } = string.Empty;
}
