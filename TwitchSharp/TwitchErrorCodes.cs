namespace TwitchSharp;

/// <summary>
/// Stable string constants for programmatic error handling in TwitchSharp.
/// </summary>
public static class TwitchErrorCodes
{
    /// <summary>The request lacked valid authentication credentials.</summary>
    public const string Unauthorized = "UNAUTHORIZED";

    /// <summary>The authenticated user does not have permission for this action.</summary>
    public const string Forbidden = "FORBIDDEN";

    /// <summary>The requested resource was not found.</summary>
    public const string NotFound = "NOT_FOUND";

    /// <summary>The request was rejected due to invalid parameters.</summary>
    public const string BadRequest = "BAD_REQUEST";

    /// <summary>The request was throttled by the Twitch API rate limiter.</summary>
    public const string RateLimited = "RATE_LIMITED";

    /// <summary>A network-level error occurred (DNS, connection, etc.).</summary>
    public const string NetworkError = "NETWORK_ERROR";

    /// <summary>The request timed out before receiving a response.</summary>
    public const string Timeout = "TIMEOUT";

    /// <summary>The response could not be deserialized into the expected type.</summary>
    public const string DeserializationError = "DESERIALIZATION_ERROR";

    /// <summary>The Twitch API returned a 5xx server error.</summary>
    public const string ServerError = "SERVER_ERROR";

    /// <summary>The authenticated token lacks required scopes for this endpoint.</summary>
    public const string InsufficientScopes = "INSUFFICIENT_SCOPES";

    /// <summary>Pagination exceeded the configured maximum page count or item count.</summary>
    public const string PaginationLimitReached = "PAGINATION_LIMIT_REACHED";

    /// <summary>Pagination detected a repeated cursor indicating an infinite loop.</summary>
    public const string PaginationCursorLoop = "PAGINATION_CURSOR_LOOP";

    /// <summary>The local rate limit queue is full and cannot accept more requests.</summary>
    public const string LocalRateLimitQueueFull = "LOCAL_RATE_LIMIT_QUEUE_FULL";

    /// <summary>The token persistence callback threw an exception.</summary>
    public const string TokenPersistenceFailed = "TOKEN_PERSISTENCE_FAILED";

    /// <summary>An unexpected or unclassified error occurred.</summary>
    public const string Unexpected = "UNEXPECTED";

    /// <summary>The request was rejected because a conflict was detected.</summary>
    public const string Conflict = "CONFLICT";

    /// <summary>The requested content is no longer available.</summary>
    public const string Gone = "GONE";

    /// <summary>The request payload exceeds the size limit.</summary>
    public const string PayloadTooLarge = "PAYLOAD_TOO_LARGE";

    /// <summary>Too many requests were sent in a given time period.</summary>
    public const string TooManyRequests = "TOO_MANY_REQUESTS";

    /// <summary>The request content type is not supported.</summary>
    public const string UnsupportedMediaType = "UNSUPPORTED_MEDIA_TYPE";
}
