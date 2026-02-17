using System.Net;

namespace TwitchSharp;

/// <summary>
/// Represents an error from the Twitch API or the TwitchSharp client infrastructure.
/// </summary>
public class TwitchApiException : Exception
{
    /// <summary>
    /// A stable error code from <see cref="TwitchErrorCodes"/> for programmatic handling.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// The HTTP status code of the response, if the error originated from an HTTP response.
    /// </summary>
    public HttpStatusCode? StatusCode { get; }

    /// <summary>
    /// A suggested retry delay, populated from rate limit or Retry-After headers.
    /// </summary>
    public TimeSpan? RetryAfter { get; }

    /// <summary>
    /// The API endpoint path that produced the error (sanitized, no query parameters or secrets).
    /// </summary>
    public string? Endpoint { get; }

    /// <summary>
    /// Gets whether this error is a rate limit error (server-side or local queue full).
    /// </summary>
    public bool IsRateLimited =>
        Code is TwitchErrorCodes.RateLimited
            or TwitchErrorCodes.LocalRateLimitQueueFull
            or TwitchErrorCodes.TooManyRequests;

    /// <summary>
    /// Gets whether this error is an authentication or authorization error.
    /// </summary>
    public bool IsUnauthorized =>
        Code is TwitchErrorCodes.Unauthorized
            or TwitchErrorCodes.Forbidden
            or TwitchErrorCodes.InsufficientScopes;

    /// <summary>
    /// Gets whether this error is transient and the request may succeed on retry.
    /// </summary>
    public bool IsTransient =>
        Code is TwitchErrorCodes.NetworkError
            or TwitchErrorCodes.Timeout
            or TwitchErrorCodes.ServerError
            or TwitchErrorCodes.RateLimited
            or TwitchErrorCodes.TooManyRequests;

    /// <summary>
    /// Initializes a new instance of the <see cref="TwitchApiException"/> class.
    /// </summary>
    /// <param name="code">A stable error code from <see cref="TwitchErrorCodes"/> for programmatic handling.</param>
    /// <param name="message">A human-readable description of the error.</param>
    /// <param name="statusCode">The HTTP status code of the response, if applicable.</param>
    /// <param name="endpoint">The API endpoint path that produced the error.</param>
    /// <param name="retryAfter">A suggested retry delay, if available from response headers.</param>
    /// <param name="innerException">The underlying exception that caused this error, if any.</param>
    public TwitchApiException(
        string code,
        string message,
        HttpStatusCode? statusCode = null,
        string? endpoint = null,
        TimeSpan? retryAfter = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
        Code = code;
        StatusCode = statusCode;
        Endpoint = endpoint;
        RetryAfter = retryAfter;
    }

    /// <summary>
    /// Attempts to get the retry delay, clamped to a maximum value.
    /// </summary>
    /// <param name="delay">The clamped retry delay.</param>
    /// <param name="maxDelay">The maximum delay to clamp to. Defaults to 2 minutes.</param>
    /// <returns><c>true</c> if a retry delay is available; otherwise <c>false</c>.</returns>
    public bool TryGetRetryDelay(out TimeSpan delay, TimeSpan? maxDelay = null)
    {
        if (RetryAfter is not { } retryAfter)
        {
            delay = TimeSpan.Zero;
            return false;
        }

        var maximumDelay = maxDelay ?? TimeSpan.FromMinutes(2);

        if (retryAfter < TimeSpan.Zero)
        {
            delay = TimeSpan.Zero;
        }
        else if (retryAfter > maximumDelay)
        {
            delay = maximumDelay;
        }
        else
        {
            delay = retryAfter;
        }

        return true;
    }
}
