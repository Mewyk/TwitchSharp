using System.Text.Json.Serialization;
using TwitchSharp.Api.Http;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Internal response wrapper for EventSub endpoints with cost metadata.
/// </summary>
internal sealed record EventSubResponse
{
    [JsonPropertyName("data")]
    public EventSubSubscriptionData[]? Data { get; init; }

    [JsonPropertyName("pagination")]
    public HelixPagination? Pagination { get; init; }

    [JsonPropertyName("total")]
    public int Total { get; init; }

    [JsonPropertyName("total_cost")]
    public int TotalCost { get; init; }

    [JsonPropertyName("max_total_cost")]
    public int MaxTotalCost { get; init; }
}
