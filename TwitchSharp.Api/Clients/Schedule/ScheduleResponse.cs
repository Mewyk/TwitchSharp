using System.Text.Json.Serialization;
using TwitchSharp.Api.Http;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Internal response wrapper for Schedule endpoints where "data" is a single object, not an array.
/// </summary>
internal sealed record ScheduleResponse
{
    [JsonPropertyName("data")]
    public ScheduleData? Data { get; init; }

    [JsonPropertyName("pagination")]
    public HelixPagination? Pagination { get; init; }
}
