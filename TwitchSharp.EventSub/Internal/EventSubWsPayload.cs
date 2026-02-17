using System.Text.Json;
using System.Text.Json.Serialization;
using TwitchSharp.Api.Clients;

namespace TwitchSharp.EventSub.Internal;

/// <summary>
/// Internal payload from a WebSocket message envelope.
/// </summary>
internal sealed record EventSubWsPayload
{
    [JsonPropertyName("session")]
    public EventSubWsSessionData? Session { get; init; }

    [JsonPropertyName("subscription")]
    public EventSubSubscriptionData? Subscription { get; init; }

    [JsonPropertyName("event")]
    public JsonElement Event { get; init; }
}
