using TwitchSharp;

namespace CoreTests;

public sealed class LogRedactionTests
{
    [Fact]
    public void RedactSecret_NullValue_ReturnsPlaceholder()
    {
        var result = LogRedaction.RedactSecret(null);

        Assert.Equal("***", result);
    }

    [Fact]
    public void RedactSecret_EmptyValue_ReturnsPlaceholder()
    {
        var result = LogRedaction.RedactSecret(string.Empty);

        Assert.Equal("***", result);
    }

    [Theory]
    [InlineData("a")]
    [InlineData("ab")]
    [InlineData("abc")]
    [InlineData("abcd")]
    public void RedactSecret_ShortValue_ReturnsPlaceholder(string value)
    {
        var result = LogRedaction.RedactSecret(value);

        Assert.Equal("***", result);
    }

    [Fact]
    public void RedactSecret_LongValue_ShowsPrefixAndPlaceholder()
    {
        var result = LogRedaction.RedactSecret("abcdefghij");

        Assert.Equal("abcd***", result);
    }

    [Fact]
    public void RedactSecret_ExactlyFiveCharacters_ShowsPrefixAndPlaceholder()
    {
        var result = LogRedaction.RedactSecret("abcde");

        Assert.Equal("abcd***", result);
    }

    [Fact]
    public void RedactFull_NullValue_ReturnsPlaceholder()
    {
        var result = LogRedaction.RedactFull(null);

        Assert.Equal("***", result);
    }

    [Fact]
    public void RedactFull_EmptyValue_ReturnsPlaceholder()
    {
        var result = LogRedaction.RedactFull(string.Empty);

        Assert.Equal("***", result);
    }

    [Fact]
    public void RedactFull_NonEmptyValue_ReturnsPlaceholder()
    {
        var result = LogRedaction.RedactFull("sensitive-data");

        Assert.Equal("***", result);
    }

    [Fact]
    public void GetSafePath_NullValue_ReturnsEmpty()
    {
        var result = LogRedaction.GetSafePath(null);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GetSafePath_EmptyValue_ReturnsEmpty()
    {
        var result = LogRedaction.GetSafePath(string.Empty);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GetSafePath_NoQueryOrFragment_ReturnsOriginal()
    {
        var result = LogRedaction.GetSafePath("https://api.twitch.tv/helix/users");

        Assert.Equal("https://api.twitch.tv/helix/users", result);
    }

    [Fact]
    public void GetSafePath_WithQueryString_StripsQuery()
    {
        var result = LogRedaction.GetSafePath("https://api.twitch.tv/helix/users?login=test");

        Assert.Equal("https://api.twitch.tv/helix/users", result);
    }

    [Fact]
    public void GetSafePath_WithFragment_StripsFragment()
    {
        var result = LogRedaction.GetSafePath("https://api.twitch.tv/helix/users#section");

        Assert.Equal("https://api.twitch.tv/helix/users", result);
    }

    [Fact]
    public void GetSafePath_WithQueryAndFragment_StripsQueryFirst()
    {
        var result = LogRedaction.GetSafePath("https://api.twitch.tv/helix/users?login=test#section");

        Assert.Equal("https://api.twitch.tv/helix/users", result);
    }
}
