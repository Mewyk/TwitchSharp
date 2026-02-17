using TwitchSharp.Api.Authentication;

namespace ApiTests;

public sealed class TwitchTokenSetTests
{
    [Fact]
    public void IsExpired_NoExpiresAt_ReturnsFalse()
    {
        var tokenSet = new TwitchTokenSet { AccessToken = "token123" };

        var result = tokenSet.IsExpired(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(60));

        Assert.False(result);
    }

    [Fact]
    public void IsExpired_NotExpired_ReturnsFalse()
    {
        var futureExpiry = DateTimeOffset.UtcNow.AddHours(1);
        var tokenSet = new TwitchTokenSet
        {
            AccessToken = "token123",
            ExpiresAtUtc = futureExpiry
        };

        var result = tokenSet.IsExpired(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(60));

        Assert.False(result);
    }

    [Fact]
    public void IsExpired_PastExpiry_ReturnsTrue()
    {
        var pastExpiry = DateTimeOffset.UtcNow.AddHours(-1);
        var tokenSet = new TwitchTokenSet
        {
            AccessToken = "token123",
            ExpiresAtUtc = pastExpiry
        };

        var result = tokenSet.IsExpired(DateTimeOffset.UtcNow, TimeSpan.Zero);

        Assert.True(result);
    }

    [Fact]
    public void IsExpired_WithinBuffer_ReturnsTrue()
    {
        var currentTime = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var expiresAt = currentTime.AddSeconds(30);
        var buffer = TimeSpan.FromSeconds(60);

        var tokenSet = new TwitchTokenSet
        {
            AccessToken = "token123",
            ExpiresAtUtc = expiresAt
        };

        var result = tokenSet.IsExpired(currentTime, buffer);

        Assert.True(result);
    }

    [Fact]
    public void IsExpired_ExactlyAtBufferBoundary_ReturnsTrue()
    {
        var currentTime = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var expiresAt = currentTime.AddSeconds(60);
        var buffer = TimeSpan.FromSeconds(60);

        var tokenSet = new TwitchTokenSet
        {
            AccessToken = "token123",
            ExpiresAtUtc = expiresAt
        };

        var result = tokenSet.IsExpired(currentTime, buffer);

        Assert.True(result);
    }

    [Fact]
    public void DefaultValues_AreCorrect()
    {
        var tokenSet = new TwitchTokenSet { AccessToken = "token123" };

        Assert.Equal("bearer", tokenSet.TokenType);
        Assert.Empty(tokenSet.Scopes);
        Assert.Null(tokenSet.RefreshToken);
        Assert.Null(tokenSet.ExpiresAtUtc);
        Assert.Null(tokenSet.IdToken);
    }
}
