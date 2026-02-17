using System.Threading.RateLimiting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TwitchSharp.Api.RateLimiting;

/// <summary>
/// Client-side rate limiter for Twitch API requests using a token bucket algorithm.
/// Twitch allows 800 requests per minute for most endpoints.
/// </summary>
internal sealed partial class TwitchRateLimiter : IAsyncDisposable
{
    private readonly TokenBucketRateLimiter _limiter;
    private readonly ILogger _logger;

    public TwitchRateLimiter(TwitchApiClientOptions options, ILoggerFactory? loggerFactory = null)
    {
        _logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<TwitchRateLimiter>();
        _limiter = new TokenBucketRateLimiter(new TokenBucketRateLimiterOptions
        {
            TokenLimit = 800,
            ReplenishmentPeriod = TimeSpan.FromMinutes(1),
            TokensPerPeriod = 800,
            AutoReplenishment = true,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = options.RateLimitQueueLimit
        });
    }

    [LoggerMessage(Level = LogLevel.Warning, Message = "Rate limit queue full for endpoint {Endpoint}")]
    private partial void LogRateLimitQueueFull(string? endpoint);

    /// <summary>
    /// Acquires a rate limit lease, waiting if necessary.
    /// Throws if the queue is full.
    /// </summary>
    public async ValueTask<RateLimitLease> AcquireAsync(string? endpoint, CancellationToken cancellationToken)
    {
        var lease = await _limiter.AcquireAsync(1, cancellationToken).ConfigureAwait(false);

        if (!lease.IsAcquired)
        {
            lease.Dispose();

            TimeSpan? retryAfter = null;
            if (lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfterValue))
            {
                retryAfter = retryAfterValue;
            }

            LogRateLimitQueueFull(endpoint);
            throw TwitchErrorMapper.FromLocalRateLimitQueueFull(endpoint, retryAfter);
        }

        return lease;
    }

    /// <summary>
    /// Gets statistics about the current rate limiter state.
    /// </summary>
    public RateLimiterStatistics? GetStatistics() => _limiter.GetStatistics();

    /// <inheritdoc />
    public ValueTask DisposeAsync()
    {
        _limiter.Dispose();
        return ValueTask.CompletedTask;
    }
}
