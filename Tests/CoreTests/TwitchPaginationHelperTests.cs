using TwitchSharp;

namespace CoreTests;

public sealed class TwitchPaginationHelperTests
{
    [Fact]
    public async Task EnumerateAllAsync_SinglePage_ReturnsAllItems()
    {
        var page = new TwitchPage<string>(["a", "b", "c"], null);

        var items = new List<string>();
        await foreach (var item in TwitchPaginationHelper.EnumerateAllAsync(
            (cursor, cancellationToken) => Task.FromResult(page),
            cancellationToken: TestContext.Current.CancellationToken))
        {
            items.Add(item);
        }

        Assert.Equal(["a", "b", "c"], items);
    }

    [Fact]
    public async Task EnumerateAllAsync_MultiplePages_ReturnsAllItemsInOrder()
    {
        var pageIndex = 0;
        var pages = new[]
        {
            new TwitchPage<int>([1, 2], "cursor1"),
            new TwitchPage<int>([3, 4], "cursor2"),
            new TwitchPage<int>([5], null)
        };

        var items = new List<int>();
        await foreach (var item in TwitchPaginationHelper.EnumerateAllAsync(
            (cursor, cancellationToken) => Task.FromResult(pages[pageIndex++]),
            cancellationToken: TestContext.Current.CancellationToken))
        {
            items.Add(item);
        }

        Assert.Equal([1, 2, 3, 4, 5], items);
    }

    [Fact]
    public async Task EnumerateAllAsync_EmptyPage_StopsEnumeration()
    {
        var fetchCount = 0;

        var items = new List<string>();
        await foreach (var item in TwitchPaginationHelper.EnumerateAllAsync<string>(
            (cursor, cancellationToken) =>
            {
                fetchCount++;
                return Task.FromResult(new TwitchPage<string>([], "some_cursor"));
            },
            new TwitchPaginationOptions { StopOnEmptyPage = true },
            cancellationToken: TestContext.Current.CancellationToken))
        {
            items.Add(item);
        }

        Assert.Empty(items);
        Assert.Equal(1, fetchCount);
    }

    [Fact]
    public async Task EnumerateAllAsync_MaxPagesExceeded_ThrowsPaginationException()
    {
        var options = new TwitchPaginationOptions { MaxPages = 2 };

        var exception = await Assert.ThrowsAsync<TwitchPaginationException>(async () =>
        {
            await foreach (var item in TwitchPaginationHelper.EnumerateAllAsync(
                (cursor, cancellationToken) =>
                    Task.FromResult(new TwitchPage<string>(["item"], "next")),
                options,
                cancellationToken: TestContext.Current.CancellationToken))
            {
                // consume items
            }
        });

        Assert.Equal(TwitchPaginationFailureReason.MaxPagesExceeded, exception.Reason);
        Assert.Equal(2, exception.PagesFetched);
    }

    [Fact]
    public async Task EnumerateAllAsync_MaxItemsExceeded_ThrowsPaginationException()
    {
        var options = new TwitchPaginationOptions { MaxItems = 3 };
        var pageIndex = 0;
        var pages = new[]
        {
            new TwitchPage<string>(["a", "b"], "cursor1"),
            new TwitchPage<string>(["c", "d", "e"], "cursor2")
        };

        var exception = await Assert.ThrowsAsync<TwitchPaginationException>(async () =>
        {
            await foreach (var item in TwitchPaginationHelper.EnumerateAllAsync(
                (cursor, cancellationToken) => Task.FromResult(pages[pageIndex++]),
                options,
                cancellationToken: TestContext.Current.CancellationToken))
            {
                // consume items
            }
        });

        Assert.Equal(TwitchPaginationFailureReason.MaxItemsExceeded, exception.Reason);
    }

    [Fact]
    public async Task EnumerateAllAsync_RepeatedCursor_ThrowsPaginationException()
    {
        var options = new TwitchPaginationOptions { StopOnRepeatedCursor = true };
        var callCount = 0;

        var exception = await Assert.ThrowsAsync<TwitchPaginationException>(async () =>
        {
            await foreach (var item in TwitchPaginationHelper.EnumerateAllAsync(
                (cursor, cancellationToken) =>
                {
                    callCount++;
                    return Task.FromResult(new TwitchPage<string>(["item"], "same_cursor"));
                },
                options,
                cancellationToken: TestContext.Current.CancellationToken))
            {
                // consume items
            }
        });

        Assert.Equal(TwitchPaginationFailureReason.RepeatedCursor, exception.Reason);
    }

    [Fact]
    public async Task EnumerateAllAsync_CancellationRequested_ThrowsOperationCanceled()
    {
        using var cancellationTokenSource = new CancellationTokenSource();
        await cancellationTokenSource.CancelAsync();

        await Assert.ThrowsAsync<OperationCanceledException>(async () =>
        {
            await foreach (var item in TwitchPaginationHelper.EnumerateAllAsync(
                (cursor, cancellationToken) =>
                    Task.FromResult(new TwitchPage<string>(["item"], "next")),
                cancellationToken: cancellationTokenSource.Token))
            {
                // should not reach here
            }
        });
    }

    [Fact]
    public async Task EnumerateAllAsync_DefaultOptions_UsesDefaultValues()
    {
        var page = new TwitchPage<string>(["item"], null);

        var items = new List<string>();
        await foreach (var item in TwitchPaginationHelper.EnumerateAllAsync(
            (cursor, cancellationToken) => Task.FromResult(page),
            cancellationToken: TestContext.Current.CancellationToken))
        {
            items.Add(item);
        }

        Assert.Single(items);
    }
}
