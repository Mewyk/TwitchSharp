using TwitchSharp;

namespace CoreTests;

public sealed class TwitchPaginationExceptionTests
{
    [Fact]
    public void MaxPagesExceeded_MapsToLimitReachedCode()
    {
        var exception = new TwitchPaginationException(
            TwitchPaginationFailureReason.MaxPagesExceeded,
            pagesFetched: 100,
            itemsFetched: 5000);

        Assert.Equal(TwitchErrorCodes.PaginationLimitReached, exception.Code);
    }

    [Fact]
    public void MaxItemsExceeded_MapsToLimitReachedCode()
    {
        var exception = new TwitchPaginationException(
            TwitchPaginationFailureReason.MaxItemsExceeded,
            pagesFetched: 50,
            itemsFetched: 10000);

        Assert.Equal(TwitchErrorCodes.PaginationLimitReached, exception.Code);
    }

    [Fact]
    public void RepeatedCursor_MapsToCursorLoopCode()
    {
        var exception = new TwitchPaginationException(
            TwitchPaginationFailureReason.RepeatedCursor,
            pagesFetched: 10,
            itemsFetched: 200);

        Assert.Equal(TwitchErrorCodes.PaginationCursorLoop, exception.Code);
    }

    [Fact]
    public void Constructor_SetsAllProperties()
    {
        var exception = new TwitchPaginationException(
            TwitchPaginationFailureReason.MaxPagesExceeded,
            pagesFetched: 42,
            itemsFetched: 1234,
            endpoint: "/helix/users");

        Assert.Equal(TwitchPaginationFailureReason.MaxPagesExceeded, exception.Reason);
        Assert.Equal(42, exception.PagesFetched);
        Assert.Equal(1234, exception.ItemsFetched);
        Assert.Equal("/helix/users", exception.Endpoint);
    }

    [Fact]
    public void MaxPagesExceeded_MessageContainsCounts()
    {
        var exception = new TwitchPaginationException(
            TwitchPaginationFailureReason.MaxPagesExceeded,
            pagesFetched: 100,
            itemsFetched: 5000);

        Assert.Contains("100", exception.Message);
        Assert.Contains("5000", exception.Message);
        Assert.Contains("maximum page count", exception.Message);
    }

    [Fact]
    public void MaxItemsExceeded_MessageContainsCounts()
    {
        var exception = new TwitchPaginationException(
            TwitchPaginationFailureReason.MaxItemsExceeded,
            pagesFetched: 50,
            itemsFetched: 10000);

        Assert.Contains("50", exception.Message);
        Assert.Contains("10000", exception.Message);
        Assert.Contains("maximum item count", exception.Message);
    }

    [Fact]
    public void RepeatedCursor_MessageContainsCounts()
    {
        var exception = new TwitchPaginationException(
            TwitchPaginationFailureReason.RepeatedCursor,
            pagesFetched: 10,
            itemsFetched: 200);

        Assert.Contains("10", exception.Message);
        Assert.Contains("200", exception.Message);
        Assert.Contains("repeated cursor", exception.Message);
    }
}
