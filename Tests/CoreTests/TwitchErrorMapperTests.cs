using System.Net;
using System.Text.Json;
using TwitchSharp;

namespace CoreTests;

public sealed class TwitchErrorMapperTests
{
    [Theory]
    [InlineData(HttpStatusCode.BadRequest, TwitchErrorCodes.BadRequest)]
    [InlineData(HttpStatusCode.Unauthorized, TwitchErrorCodes.Unauthorized)]
    [InlineData(HttpStatusCode.Forbidden, TwitchErrorCodes.Forbidden)]
    [InlineData(HttpStatusCode.NotFound, TwitchErrorCodes.NotFound)]
    [InlineData(HttpStatusCode.Conflict, TwitchErrorCodes.Conflict)]
    [InlineData(HttpStatusCode.Gone, TwitchErrorCodes.Gone)]
    [InlineData(HttpStatusCode.RequestEntityTooLarge, TwitchErrorCodes.PayloadTooLarge)]
    [InlineData(HttpStatusCode.UnsupportedMediaType, TwitchErrorCodes.UnsupportedMediaType)]
    [InlineData(HttpStatusCode.TooManyRequests, TwitchErrorCodes.TooManyRequests)]
    [InlineData(HttpStatusCode.RequestTimeout, TwitchErrorCodes.Timeout)]
    [InlineData(HttpStatusCode.InternalServerError, TwitchErrorCodes.ServerError)]
    public void MapHttpStatus_KnownStatusCode_ReturnsExpectedCode(HttpStatusCode statusCode, string expectedCode)
    {
        var result = TwitchErrorMapper.MapHttpStatus(statusCode);

        Assert.Equal(expectedCode, result);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadGateway)]
    [InlineData(HttpStatusCode.ServiceUnavailable)]
    [InlineData(HttpStatusCode.GatewayTimeout)]
    public void MapHttpStatus_ServerErrorRange_ReturnsServerError(HttpStatusCode statusCode)
    {
        var result = TwitchErrorMapper.MapHttpStatus(statusCode);

        Assert.Equal(TwitchErrorCodes.ServerError, result);
    }

    [Fact]
    public void MapHttpStatus_UnknownStatusCode_ReturnsUnexpected()
    {
        var result = TwitchErrorMapper.MapHttpStatus(HttpStatusCode.Ambiguous);

        Assert.Equal(TwitchErrorCodes.Unexpected, result);
    }

    [Fact]
    public void FromHttpResponse_SetsAllProperties()
    {
        var retryAfter = TimeSpan.FromSeconds(30);

        var exception = TwitchErrorMapper.FromHttpResponse(
            HttpStatusCode.TooManyRequests,
            "/helix/users",
            "Rate limit exceeded",
            retryAfter);

        Assert.Equal(TwitchErrorCodes.TooManyRequests, exception.Code);
        Assert.Equal("Rate limit exceeded", exception.Message);
        Assert.Equal(HttpStatusCode.TooManyRequests, exception.StatusCode);
        Assert.Equal("/helix/users", exception.Endpoint);
        Assert.Equal(retryAfter, exception.RetryAfter);
    }

    [Fact]
    public void FromHttpResponse_NullMessage_FormatsDefaultMessage()
    {
        var exception = TwitchErrorMapper.FromHttpResponse(
            HttpStatusCode.NotFound,
            "/helix/users");

        Assert.Contains("404", exception.Message);
        Assert.Contains("NotFound", exception.Message);
    }

    [Fact]
    public void FromNetworkException_TaskCanceledException_SetsTimeoutCode()
    {
        var innerException = new TaskCanceledException("The request timed out.");

        var exception = TwitchErrorMapper.FromNetworkException(innerException, "/helix/users");

        Assert.Equal(TwitchErrorCodes.Timeout, exception.Code);
        Assert.Equal("The request timed out.", exception.Message);
        Assert.Same(innerException, exception.InnerException);
        Assert.Equal("/helix/users", exception.Endpoint);
    }

    [Fact]
    public void FromNetworkException_HttpRequestException_SetsNetworkErrorCode()
    {
        var innerException = new HttpRequestException("Connection refused");

        var exception = TwitchErrorMapper.FromNetworkException(innerException, "/helix/streams");

        Assert.Equal(TwitchErrorCodes.NetworkError, exception.Code);
        Assert.Equal("Connection refused", exception.Message);
        Assert.Same(innerException, exception.InnerException);
    }

    [Fact]
    public void FromNetworkException_GenericException_SetsNetworkErrorCode()
    {
        var innerException = new InvalidOperationException("Something went wrong");

        var exception = TwitchErrorMapper.FromNetworkException(innerException, "/helix/chat");

        Assert.Equal(TwitchErrorCodes.NetworkError, exception.Code);
        Assert.Same(innerException, exception.InnerException);
    }

    [Fact]
    public void FromDeserializationError_SetsDeserializationErrorCode()
    {
        var innerException = new JsonException("Invalid JSON");

        var exception = TwitchErrorMapper.FromDeserializationError(innerException, "/helix/users");

        Assert.Equal(TwitchErrorCodes.DeserializationError, exception.Code);
        Assert.Contains("Failed to deserialize", exception.Message);
        Assert.Same(innerException, exception.InnerException);
        Assert.Equal("/helix/users", exception.Endpoint);
    }

    [Fact]
    public void FromLocalRateLimitQueueFull_SetsCorrectCode()
    {
        var retryAfter = TimeSpan.FromSeconds(5);

        var exception = TwitchErrorMapper.FromLocalRateLimitQueueFull("/helix/users", retryAfter);

        Assert.Equal(TwitchErrorCodes.LocalRateLimitQueueFull, exception.Code);
        Assert.Contains("queue is full", exception.Message);
        Assert.Equal("/helix/users", exception.Endpoint);
        Assert.Equal(retryAfter, exception.RetryAfter);
    }

    [Fact]
    public void FromTokenPersistenceFailed_SetsCorrectCode()
    {
        var innerException = new InvalidOperationException("Save failed");

        var exception = TwitchErrorMapper.FromTokenPersistenceFailed(innerException);

        Assert.Equal(TwitchErrorCodes.TokenPersistenceFailed, exception.Code);
        Assert.Contains("persist", exception.Message);
        Assert.Same(innerException, exception.InnerException);
    }

    [Fact]
    public void FromTokenPersistenceFailed_NoInnerException_SetsCorrectCode()
    {
        var exception = TwitchErrorMapper.FromTokenPersistenceFailed();

        Assert.Equal(TwitchErrorCodes.TokenPersistenceFailed, exception.Code);
        Assert.Null(exception.InnerException);
    }
}
