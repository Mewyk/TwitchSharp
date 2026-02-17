namespace TwitchSharp;

/// <summary>
/// Configuration options for controlling pagination safety bounds.
/// </summary>
public sealed class TwitchPaginationOptions
{
    /// <summary>
    /// Default pagination options with conservative safety bounds.
    /// </summary>
    public static TwitchPaginationOptions Default { get; } = new();

    /// <summary>
    /// The maximum number of pages to fetch. Set to <c>null</c> for no limit.
    /// Default is 1,000.
    /// </summary>
    public int? MaxPages { get; init; } = 1_000;

    /// <summary>
    /// The maximum number of items to yield. Set to <c>null</c> for no limit.
    /// Default is 100,000.
    /// </summary>
    public int? MaxItems { get; init; } = 100_000;

    /// <summary>
    /// Whether to stop enumeration when an empty page is returned.
    /// Default is <c>true</c>.
    /// </summary>
    public bool StopOnEmptyPage { get; init; } = true;

    /// <summary>
    /// Whether to throw a <see cref="TwitchPaginationException"/> when a repeated cursor is detected.
    /// Default is <c>true</c>.
    /// </summary>
    public bool StopOnRepeatedCursor { get; init; } = true;
}
