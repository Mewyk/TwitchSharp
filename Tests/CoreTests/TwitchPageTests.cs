using TwitchSharp;

namespace CoreTests;

public sealed class TwitchPageTests
{
    [Fact]
    public void Constructor_SetsDataAndCursor()
    {
        var data = new[] { "item1", "item2" };
        var cursor = "next_cursor";

        var page = new TwitchPage<string>(data, cursor);

        Assert.Equal(data, page.Data);
        Assert.Equal(cursor, page.Cursor);
    }

    [Fact]
    public void Empty_HasNoData()
    {
        var page = TwitchPage<string>.Empty;

        Assert.Empty(page.Data);
    }

    [Fact]
    public void Empty_HasNullCursor()
    {
        var page = TwitchPage<string>.Empty;

        Assert.Null(page.Cursor);
    }

    [Fact]
    public void HasMore_WithCursor_ReturnsTrue()
    {
        var page = new TwitchPage<string>(["item"], "cursor_value");

        Assert.True(page.HasMore);
    }

    [Fact]
    public void HasMore_WithNullCursor_ReturnsFalse()
    {
        var page = new TwitchPage<string>(["item"], null);

        Assert.False(page.HasMore);
    }

    [Fact]
    public void HasMore_WithEmptyCursor_ReturnsFalse()
    {
        var page = new TwitchPage<string>(["item"], string.Empty);

        Assert.False(page.HasMore);
    }
}
