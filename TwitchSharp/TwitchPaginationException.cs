namespace TwitchSharp;

/// <summary>
/// The reason a pagination operation was terminated.
/// </summary>
public enum TwitchPaginationFailureReason
{
    /// <summary>The configured maximum number of pages was exceeded.</summary>
    MaxPagesExceeded,

    /// <summary>The configured maximum number of items was exceeded.</summary>
    MaxItemsExceeded,

    /// <summary>A repeated cursor was detected, indicating an infinite loop.</summary>
    RepeatedCursor
}

/// <summary>
/// Represents an error during paginated enumeration of Twitch API results.
/// </summary>
public sealed class TwitchPaginationException : TwitchApiException
{
    /// <summary>
    /// The reason pagination was terminated.
    /// </summary>
    public TwitchPaginationFailureReason Reason { get; }

    /// <summary>
    /// The number of pages successfully fetched before the failure.
    /// </summary>
    public int PagesFetched { get; }

    /// <summary>
    /// The number of items successfully yielded before the failure.
    /// </summary>
    public int ItemsFetched { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TwitchPaginationException"/> class.
    /// </summary>
    public TwitchPaginationException(
        TwitchPaginationFailureReason reason,
        int pagesFetched,
        int itemsFetched,
        string? endpoint = null)
        : base(
            MapReasonToCode(reason),
            FormatMessage(reason, pagesFetched, itemsFetched),
            endpoint: endpoint)
    {
        Reason = reason;
        PagesFetched = pagesFetched;
        ItemsFetched = itemsFetched;
    }

    private static string MapReasonToCode(TwitchPaginationFailureReason reason) =>
        reason switch
        {
            TwitchPaginationFailureReason.RepeatedCursor => TwitchErrorCodes.PaginationCursorLoop,
            _ => TwitchErrorCodes.PaginationLimitReached
        };

    private static string FormatMessage(TwitchPaginationFailureReason reason, int pagesFetched, int itemsFetched) =>
        reason switch
        {
            TwitchPaginationFailureReason.MaxPagesExceeded =>
                $"Pagination stopped after {pagesFetched} pages ({itemsFetched} items): maximum page count exceeded.",
            TwitchPaginationFailureReason.MaxItemsExceeded =>
                $"Pagination stopped after {pagesFetched} pages ({itemsFetched} items): maximum item count exceeded.",
            TwitchPaginationFailureReason.RepeatedCursor =>
                $"Pagination stopped after {pagesFetched} pages ({itemsFetched} items): repeated cursor detected.",
            _ =>
                $"Pagination stopped after {pagesFetched} pages ({itemsFetched} items)."
        };
}
