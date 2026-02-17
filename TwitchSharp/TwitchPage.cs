namespace TwitchSharp;

/// <summary>
/// Represents a single page of results from a paginated Twitch API endpoint.
/// </summary>
/// <typeparam name="T">The type of items in the page.</typeparam>
public sealed class TwitchPage<T>
{
    /// <summary>
    /// An empty page with no items and no cursor.
    /// </summary>
    public static TwitchPage<T> Empty { get; } = new([], null);

    /// <summary>
    /// The items in this page.
    /// </summary>
    public IReadOnlyList<T> Data { get; }

    /// <summary>
    /// The cursor for fetching the next page, or <c>null</c> if this is the last page.
    /// </summary>
    public string? Cursor { get; }

    /// <summary>
    /// Gets whether there are more pages available.
    /// </summary>
    public bool HasMore => !string.IsNullOrEmpty(Cursor);

    /// <summary>
    /// Initializes a new instance of the <see cref="TwitchPage{T}"/> class.
    /// </summary>
    /// <param name="data">The items in this page.</param>
    /// <param name="cursor">The pagination cursor for the next page.</param>
    public TwitchPage(IReadOnlyList<T> data, string? cursor)
    {
        Data = data;
        Cursor = cursor;
    }
}
