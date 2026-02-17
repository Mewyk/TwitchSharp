namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the result of a Bits Leaderboard query, including entries, date range, and total count.
/// </summary>
public sealed record BitsLeaderboardResult
{
    /// <summary>The leaderboard entries ordered by cheer amount.</summary>
    public BitsLeaderboardEntryData[] Entries { get; init; } = [];

    /// <summary>The start date of the reporting window (RFC3339 format).</summary>
    public string DateRangeStartedAt { get; init; } = string.Empty;

    /// <summary>The end date of the reporting window (RFC3339 format).</summary>
    public string DateRangeEndedAt { get; init; } = string.Empty;

    /// <summary>The total number of ranked users returned.</summary>
    public int Total { get; init; }
}
