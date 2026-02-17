using System.Runtime.CompilerServices;

namespace TwitchSharp;

/// <summary>
/// Provides helpers for iterating paginated Twitch API endpoints as <see cref="IAsyncEnumerable{T}"/>.
/// </summary>
public static class TwitchPaginationHelper
{
    /// <summary>
    /// Enumerates all items across paginated responses from a Twitch API endpoint.
    /// </summary>
    /// <typeparam name="T">The type of items returned by the endpoint.</typeparam>
    /// <param name="fetchPage">
    /// A function that fetches a single page given an optional cursor and cancellation token.
    /// Pass <c>null</c> as the cursor for the first page.
    /// </param>
    /// <param name="options">
    /// Optional pagination safety bounds. Uses <see cref="TwitchPaginationOptions.Default"/> if not specified.
    /// </param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>An async enumerable of all items across all pages.</returns>
    public static async IAsyncEnumerable<T> EnumerateAllAsync<T>(
        Func<string?, CancellationToken, Task<TwitchPage<T>>> fetchPage,
        TwitchPaginationOptions? options = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        options ??= TwitchPaginationOptions.Default;

        string? cursor = null;
        string? previousCursor = null;
        int pageCount = 0;
        int itemCount = 0;

        do
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Check max pages before fetching
            if (options.MaxPages is { } maxPages && pageCount >= maxPages)
            {
                throw new TwitchPaginationException(
                    TwitchPaginationFailureReason.MaxPagesExceeded,
                    pageCount,
                    itemCount);
            }

            var page = await fetchPage(cursor, cancellationToken).ConfigureAwait(false);
            pageCount++;

            // Check for empty page
            if (page.Data.Count == 0 && options.StopOnEmptyPage)
            {
                yield break;
            }

            // Check for repeated cursor
            if (options.StopOnRepeatedCursor
                && cursor is not null
                && string.Equals(page.Cursor, previousCursor, StringComparison.Ordinal))
            {
                throw new TwitchPaginationException(
                    TwitchPaginationFailureReason.RepeatedCursor,
                    pageCount,
                    itemCount);
            }

            // Yield items
            foreach (var item in page.Data)
            {
                cancellationToken.ThrowIfCancellationRequested();

                itemCount++;

                if (options.MaxItems is { } maxItems && itemCount > maxItems)
                {
                    throw new TwitchPaginationException(
                        TwitchPaginationFailureReason.MaxItemsExceeded,
                        pageCount,
                        itemCount - 1);
                }

                yield return item;
            }

            previousCursor = cursor;
            cursor = page.Cursor;

        } while (!string.IsNullOrEmpty(cursor));
    }
}
