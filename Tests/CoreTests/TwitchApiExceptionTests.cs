using System.Net;
using TwitchSharp;

namespace CoreTests;

public sealed class TwitchApiExceptionTests
{
    [Fact]
    public void Constructor_SetsAllProperties()
    {
        var retryAfter = TimeSpan.FromSeconds(10);
        var innerException = new InvalidOperationException("inner");

        var exception = new TwitchApiException(
            TwitchErrorCodes.BadRequest,
            "Bad request",
            HttpStatusCode.BadRequest,
            "/helix/users",
            retryAfter,
            innerException);

        Assert.Equal(TwitchErrorCodes.BadRequest, exception.Code);
        Assert.Equal("Bad request", exception.Message);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        Assert.Equal("/helix/users", exception.Endpoint);
        Assert.Equal(retryAfter, exception.RetryAfter);
        Assert.Same(innerException, exception.InnerException);
    }

    [Theory]
    [InlineData(TwitchErrorCodes.RateLimited)]
    [InlineData(TwitchErrorCodes.LocalRateLimitQueueFull)]
    [InlineData(TwitchErrorCodes.TooManyRequests)]
    public void IsRateLimited_WithRateLimitCode_ReturnsTrue(string code)
    {
        var exception = new TwitchApiException(code, "test");

        Assert.True(exception.IsRateLimited);
    }

    [Theory]
    [InlineData(TwitchErrorCodes.BadRequest)]
    [InlineData(TwitchErrorCodes.Unauthorized)]
    [InlineData(TwitchErrorCodes.ServerError)]
    [InlineData(TwitchErrorCodes.NetworkError)]
    public void IsRateLimited_WithNonRateLimitCode_ReturnsFalse(string code)
    {
        var exception = new TwitchApiException(code, "test");

        Assert.False(exception.IsRateLimited);
    }

    [Theory]
    [InlineData(TwitchErrorCodes.Unauthorized)]
    [InlineData(TwitchErrorCodes.Forbidden)]
    [InlineData(TwitchErrorCodes.InsufficientScopes)]
    public void IsUnauthorized_WithAuthCode_ReturnsTrue(string code)
    {
        var exception = new TwitchApiException(code, "test");

        Assert.True(exception.IsUnauthorized);
    }

    [Theory]
    [InlineData(TwitchErrorCodes.BadRequest)]
    [InlineData(TwitchErrorCodes.RateLimited)]
    [InlineData(TwitchErrorCodes.NotFound)]
    public void IsUnauthorized_WithNonAuthCode_ReturnsFalse(string code)
    {
        var exception = new TwitchApiException(code, "test");

        Assert.False(exception.IsUnauthorized);
    }

    [Theory]
    [InlineData(TwitchErrorCodes.NetworkError)]
    [InlineData(TwitchErrorCodes.Timeout)]
    [InlineData(TwitchErrorCodes.ServerError)]
    [InlineData(TwitchErrorCodes.RateLimited)]
    [InlineData(TwitchErrorCodes.TooManyRequests)]
    public void IsTransient_WithTransientCode_ReturnsTrue(string code)
    {
        var exception = new TwitchApiException(code, "test");

        Assert.True(exception.IsTransient);
    }

    [Theory]
    [InlineData(TwitchErrorCodes.BadRequest)]
    [InlineData(TwitchErrorCodes.Unauthorized)]
    [InlineData(TwitchErrorCodes.NotFound)]
    [InlineData(TwitchErrorCodes.DeserializationError)]
    public void IsTransient_WithNonTransientCode_ReturnsFalse(string code)
    {
        var exception = new TwitchApiException(code, "test");

        Assert.False(exception.IsTransient);
    }

    [Fact]
    public void TryGetRetryDelay_NoRetryAfter_ReturnsFalse()
    {
        var exception = new TwitchApiException(TwitchErrorCodes.ServerError, "test");

        var result = exception.TryGetRetryDelay(out var delay);

        Assert.False(result);
        Assert.Equal(TimeSpan.Zero, delay);
    }

    [Fact]
    public void TryGetRetryDelay_WithRetryAfter_ReturnsTrueAndValue()
    {
        var retryAfter = TimeSpan.FromSeconds(30);
        var exception = new TwitchApiException(
            TwitchErrorCodes.RateLimited,
            "test",
            retryAfter: retryAfter);

        var result = exception.TryGetRetryDelay(out var delay);

        Assert.True(result);
        Assert.Equal(retryAfter, delay);
    }

    [Fact]
    public void TryGetRetryDelay_NegativeRetryAfter_ClampsToZero()
    {
        var exception = new TwitchApiException(
            TwitchErrorCodes.RateLimited,
            "test",
            retryAfter: TimeSpan.FromSeconds(-5));

        var result = exception.TryGetRetryDelay(out var delay);

        Assert.True(result);
        Assert.Equal(TimeSpan.Zero, delay);
    }

    [Fact]
    public void TryGetRetryDelay_ExceedsDefaultMax_ClampsToTwoMinutes()
    {
        var exception = new TwitchApiException(
            TwitchErrorCodes.RateLimited,
            "test",
            retryAfter: TimeSpan.FromMinutes(10));

        var result = exception.TryGetRetryDelay(out var delay);

        Assert.True(result);
        Assert.Equal(TimeSpan.FromMinutes(2), delay);
    }

    [Fact]
    public void TryGetRetryDelay_CustomMaxDelay_UsesCustomMax()
    {
        var exception = new TwitchApiException(
            TwitchErrorCodes.RateLimited,
            "test",
            retryAfter: TimeSpan.FromSeconds(60));

        var customMax = TimeSpan.FromSeconds(30);
        var result = exception.TryGetRetryDelay(out var delay, customMax);

        Assert.True(result);
        Assert.Equal(customMax, delay);
    }
}
