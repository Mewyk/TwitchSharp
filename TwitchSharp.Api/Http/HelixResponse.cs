using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Http;

/// <summary>
/// The standard Helix API response envelope containing a data array and optional pagination.
/// </summary>
/// <typeparam name="T">The type of items in the data array.</typeparam>
internal sealed record HelixDataResponse<T>
{
    [JsonPropertyName("data")]
    public T[]? Data { get; init; }

    [JsonPropertyName("pagination")]
    public HelixPagination? Pagination { get; init; }

    [JsonPropertyName("total")]
    public int? Total { get; init; }

    [JsonPropertyName("template")]
    public string? Template { get; init; }

    [JsonPropertyName("points")]
    public int? Points { get; init; }

    [JsonPropertyName("date_range")]
    public HelixDateRange? DateRange { get; init; }
}

/// <summary>
/// Date range information from a Helix API response (e.g., Bits Leaderboard).
/// </summary>
internal sealed record HelixDateRange
{
    [JsonPropertyName("started_at")]
    public string StartedAt { get; init; } = string.Empty;

    [JsonPropertyName("ended_at")]
    public string EndedAt { get; init; } = string.Empty;
}

/// <summary>
/// Pagination cursor information from a Helix API response.
/// </summary>
internal sealed record HelixPagination
{
    [JsonPropertyName("cursor")]
    public string? Cursor { get; init; }
}

/// <summary>
/// Error response body from the Twitch API.
/// </summary>
internal sealed record TwitchErrorResponse
{
    [JsonPropertyName("error")]
    public string? Error { get; init; }

    [JsonPropertyName("status")]
    public int Status { get; init; }

    [JsonPropertyName("message")]
    public string? Message { get; init; }
}
