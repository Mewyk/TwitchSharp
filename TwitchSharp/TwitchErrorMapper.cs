using System.Net;

namespace TwitchSharp;

/// <summary>
/// Factory methods for constructing <see cref="TwitchApiException"/> from various error sources.
/// </summary>
public static class TwitchErrorMapper
{
    /// <summary>
    /// Maps an HTTP status code to a <see cref="TwitchErrorCodes"/> string.
    /// </summary>
    /// <param name="statusCode">The HTTP status code to map.</param>
    /// <returns>The corresponding <see cref="TwitchErrorCodes"/> constant.</returns>
    public static string MapHttpStatus(HttpStatusCode statusCode) =>
        statusCode switch
        {
            HttpStatusCode.BadRequest => TwitchErrorCodes.BadRequest,
            HttpStatusCode.Unauthorized => TwitchErrorCodes.Unauthorized,
            HttpStatusCode.Forbidden => TwitchErrorCodes.Forbidden,
            HttpStatusCode.NotFound => TwitchErrorCodes.NotFound,
            HttpStatusCode.Conflict => TwitchErrorCodes.Conflict,
            HttpStatusCode.Gone => TwitchErrorCodes.Gone,
            HttpStatusCode.RequestEntityTooLarge => TwitchErrorCodes.PayloadTooLarge,
            HttpStatusCode.UnsupportedMediaType => TwitchErrorCodes.UnsupportedMediaType,
            HttpStatusCode.TooManyRequests => TwitchErrorCodes.TooManyRequests,
            HttpStatusCode.RequestTimeout => TwitchErrorCodes.Timeout,
            >= HttpStatusCode.InternalServerError => TwitchErrorCodes.ServerError,
            _ => TwitchErrorCodes.Unexpected
        };

    /// <summary>
    /// Creates a <see cref="TwitchApiException"/> from an HTTP response.
    /// </summary>
    /// <param name="statusCode">The HTTP status code of the response.</param>
    /// <param name="endpoint">The API endpoint path that produced the error.</param>
    /// <param name="message">An optional error message. If <c>null</c>, a default message is generated.</param>
    /// <param name="retryAfter">An optional retry delay from response headers.</param>
    /// <returns>A new <see cref="TwitchApiException"/> representing the HTTP error.</returns>
    public static TwitchApiException FromHttpResponse(
        HttpStatusCode statusCode,
        string? endpoint,
        string? message = null,
        TimeSpan? retryAfter = null)
    {
        var code = MapHttpStatus(statusCode);
        var displayMessage = message ?? $"Twitch API returned {(int)statusCode} {statusCode}.";

        return new TwitchApiException(
            code,
            displayMessage,
            statusCode,
            endpoint,
            retryAfter);
    }

    /// <summary>
    /// Creates a <see cref="TwitchApiException"/> from a network-level exception.
    /// </summary>
    /// <param name="exception">The network exception that occurred.</param>
    /// <param name="endpoint">The API endpoint path that produced the error.</param>
    /// <returns>A new <see cref="TwitchApiException"/> representing the network error.</returns>
    public static TwitchApiException FromNetworkException(Exception exception, string? endpoint)
    {
        var (code, message) = exception switch
        {
            TaskCanceledException => (TwitchErrorCodes.Timeout, "The request timed out."),
            HttpRequestException httpException => (TwitchErrorCodes.NetworkError, httpException.Message),
            _ => (TwitchErrorCodes.NetworkError, exception.Message)
        };

        return new TwitchApiException(
            code,
            message,
            endpoint: endpoint,
            innerException: exception);
    }

    /// <summary>
    /// Creates a <see cref="TwitchApiException"/> from a deserialization failure.
    /// </summary>
    /// <param name="exception">The deserialization exception that occurred.</param>
    /// <param name="endpoint">The API endpoint path that produced the error.</param>
    /// <returns>A new <see cref="TwitchApiException"/> representing the deserialization error.</returns>
    public static TwitchApiException FromDeserializationError(Exception exception, string? endpoint) =>
        new(
            TwitchErrorCodes.DeserializationError,
            $"Failed to deserialize API response: {exception.Message}",
            endpoint: endpoint,
            innerException: exception);

    /// <summary>
    /// Creates a <see cref="TwitchApiException"/> indicating the local rate limit queue is full.
    /// </summary>
    /// <param name="endpoint">The API endpoint path that produced the error.</param>
    /// <param name="retryAfter">An optional suggested retry delay.</param>
    /// <returns>A new <see cref="TwitchApiException"/> representing the rate limit queue overflow.</returns>
    public static TwitchApiException FromLocalRateLimitQueueFull(string? endpoint, TimeSpan? retryAfter = null) =>
        new(
            TwitchErrorCodes.LocalRateLimitQueueFull,
            "The local rate limit queue is full. Too many concurrent requests.",
            endpoint: endpoint,
            retryAfter: retryAfter);

    /// <summary>
    /// Creates a <see cref="TwitchApiException"/> indicating a token persistence callback failure.
    /// </summary>
    /// <param name="innerException">The exception thrown by the persistence callback, if any.</param>
    /// <returns>A new <see cref="TwitchApiException"/> representing the token persistence failure.</returns>
    public static TwitchApiException FromTokenPersistenceFailed(Exception? innerException = null) =>
        new(
            TwitchErrorCodes.TokenPersistenceFailed,
            "Failed to persist refreshed token via the configured callback.",
            innerException: innerException);
}
