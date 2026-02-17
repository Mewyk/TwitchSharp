using System.Collections.Frozen;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TwitchSharp.Api.Http;

/// <summary>
/// A <see cref="DelegatingHandler"/> that provides automatic retry with exponential backoff
/// for transient failures on idempotent requests (GET and HEAD only).
/// Handles 429 (Too Many Requests) by respecting the server-provided <c>Retry-After</c>
/// or <c>Ratelimit-Reset</c> response headers for backoff timing.
/// </summary>
internal sealed partial class TwitchResilienceHandler : DelegatingHandler
{
    private readonly int _maxRetryAttempts;
    private readonly ILogger _logger;

    private static readonly FrozenSet<HttpStatusCode> TransientStatusCodes =
        new HashSet<HttpStatusCode>
        {
            HttpStatusCode.RequestTimeout,
            HttpStatusCode.TooManyRequests,
            HttpStatusCode.InternalServerError,
            HttpStatusCode.BadGateway,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.GatewayTimeout
        }.ToFrozenSet();

    public TwitchResilienceHandler(TwitchApiClientOptions options, ILoggerFactory? loggerFactory = null)
    {
        _maxRetryAttempts = options.MaxRetryAttempts;
        _logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<TwitchResilienceHandler>();
    }

    [LoggerMessage(Level = LogLevel.Warning, Message = "HTTP request returned {StatusCode}, retrying in {DelayMs:F0}ms (attempt {Attempt}/{MaxAttempts})")]
    private partial void LogRetryAttempt(int statusCode, double delayMs, int attempt, int maxAttempts);

    [LoggerMessage(Level = LogLevel.Warning, Message = "HTTP request failed with exception, retrying (attempt {Attempt}/{MaxAttempts})")]
    private partial void LogNetworkExceptionRetry(Exception exception, int attempt, int maxAttempts);

    [LoggerMessage(Level = LogLevel.Error, Message = "All {MaxAttempts} retry attempts exhausted (last status: {StatusCode})")]
    private partial void LogRetriesExhausted(int maxAttempts, int? statusCode);

    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Only retry idempotent methods (GET/HEAD) - they have no body to clone
        if (!IsIdempotent(request.Method))
        {
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        HttpResponseMessage? response = null;

        for (int attempt = 0; attempt <= _maxRetryAttempts; attempt++)
        {
            if (attempt > 0)
            {
                // Extract server-provided retry delay before disposing the response.
                // For 429 responses, prefer Retry-After or Ratelimit-Reset headers.
                var delay = GetServerRetryDelay(response) ?? GetExponentialBackoff(attempt);

                if (delay > TimeSpan.FromSeconds(30))
                {
                    delay = TimeSpan.FromSeconds(30);
                }

                if (response is not null)
                {
                    LogRetryAttempt((int)response.StatusCode, delay.TotalMilliseconds, attempt, _maxRetryAttempts);
                }

                // Dispose previous failed response
                response?.Dispose();

                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);

                // Create a new request for the retry (HttpRequestMessage cannot be reused)
                request = CloneIdempotentRequest(request);
            }

            try
            {
                response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                if (!TransientStatusCodes.Contains(response.StatusCode))
                {
                    return response;
                }
            }
            catch (HttpRequestException exception) when (attempt < _maxRetryAttempts)
            {
                // Transient network error, will retry
                LogNetworkExceptionRetry(exception, attempt + 1, _maxRetryAttempts);
            }
            catch (TaskCanceledException exception) when (!cancellationToken.IsCancellationRequested && attempt < _maxRetryAttempts)
            {
                // Timeout (not user cancellation), will retry
                LogNetworkExceptionRetry(exception, attempt + 1, _maxRetryAttempts);
            }
        }

        // Return the last response even if it was a transient error
        LogRetriesExhausted(_maxRetryAttempts, (int?)response?.StatusCode);
        return response ?? throw new HttpRequestException("All retry attempts failed.");
    }

    private static TimeSpan GetExponentialBackoff(int attempt)
    {
        var baseDelay = TimeSpan.FromSeconds(Math.Pow(2, attempt - 1));
        var jitter = TimeSpan.FromMilliseconds(Random.Shared.Next(0, 500));
        return baseDelay + jitter;
    }

    /// <summary>
    /// Extracts a retry delay from the response's <c>Retry-After</c> or Twitch <c>Ratelimit-Reset</c> headers.
    /// Returns <c>null</c> if no server-provided timing is available.
    /// </summary>
    private static TimeSpan? GetServerRetryDelay(HttpResponseMessage? response)
    {
        if (response is null)
        {
            return null;
        }

        // Standard Retry-After header (used by many HTTP servers)
        if (response.Headers.RetryAfter is { } retryAfter)
        {
            if (retryAfter.Delta is { } delta)
            {
                return delta > TimeSpan.Zero ? delta : TimeSpan.Zero;
            }

            if (retryAfter.Date is { } date)
            {
                var delay = date - DateTimeOffset.UtcNow;
                return delay > TimeSpan.Zero ? delay : TimeSpan.Zero;
            }
        }

        // Twitch-specific Ratelimit-Reset header (Unix timestamp)
        if (response.Headers.TryGetValues("Ratelimit-Reset", out var resetValues))
        {
            var resetValue = resetValues.FirstOrDefault();
            if (long.TryParse(resetValue, out var resetTimestamp))
            {
                var resetTime = DateTimeOffset.FromUnixTimeSeconds(resetTimestamp);
                var delay = resetTime - DateTimeOffset.UtcNow;
                return delay > TimeSpan.Zero ? delay : TimeSpan.Zero;
            }
        }

        return null;
    }

    private static bool IsIdempotent(HttpMethod method) =>
        method == HttpMethod.Get || method == HttpMethod.Head;

    private static HttpRequestMessage CloneIdempotentRequest(HttpRequestMessage original)
    {
        var clone = new HttpRequestMessage(original.Method, original.RequestUri);

        foreach (var header in original.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

#if NET
        foreach (var option in original.Options)
        {
            ((IDictionary<string, object?>)clone.Options).TryAdd(option.Key, option.Value);
        }
#endif

        return clone;
    }
}
