using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using TwitchSharp.Api.Json;
using TwitchSharp.Api.RateLimiting;

namespace TwitchSharp.Api.Http;

/// <summary>
/// Internal HTTP client for Twitch Helix API requests.
/// Coordinates rate limiting, request dispatch, and error mapping.
/// </summary>
internal sealed partial class HelixHttpClient : IAsyncDisposable
{
    private readonly Func<HttpClient> _helixClientFactory;
    private readonly TwitchRateLimiter _rateLimiter;
    private readonly ILogger _logger;

    public HelixHttpClient(
        Func<HttpClient> helixClientFactory,
        TwitchRateLimiter rateLimiter,
        ILoggerFactory? loggerFactory = null)
    {
        _helixClientFactory = helixClientFactory;
        _rateLimiter = rateLimiter;
        _logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<HelixHttpClient>();
    }

    [LoggerMessage(Level = LogLevel.Debug, Message = "{Method} {Endpoint}")]
    private partial void LogRequestDispatched(string method, string endpoint);

    [LoggerMessage(Level = LogLevel.Error, Message = "{Endpoint} returned {StatusCode}: {ErrorMessage}")]
    private partial void LogErrorResponse(string endpoint, int statusCode, string? errorMessage);

    /// <summary>
    /// Sends a request and deserializes the JSON response body.
    /// </summary>
    public async Task<T> SendAsync<T>(
        HttpMethod method,
        string endpoint,
        TwitchAuthenticationMode authMode,
        JsonTypeInfo<T> responseTypeInfo,
        HttpContent? content = null,
        CancellationToken cancellationToken = default)
    {
        var safePath = LogRedaction.GetSafePath(endpoint);

        using var lease = await _rateLimiter.AcquireAsync(safePath, cancellationToken).ConfigureAwait(false);
        using var request = CreateRequest(method, endpoint, authMode, content);

        var client = _helixClientFactory();

        LogRequestDispatched(method.Method, safePath);
        HttpResponseMessage response;
        try
        {
            response = await client.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken).ConfigureAwait(false);
        }
        catch (HttpRequestException exception)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, safePath);
        }
        catch (TaskCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, safePath);
        }

        using (response)
        {
            if (!response.IsSuccessStatusCode)
            {
                await HandleErrorResponseAsync(response, safePath, cancellationToken).ConfigureAwait(false);
            }

            try
            {
                await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
                var result = await JsonSerializer.DeserializeAsync(stream, responseTypeInfo, cancellationToken).ConfigureAwait(false);

                if (result is null)
                {
                    throw TwitchErrorMapper.FromDeserializationError(
                        new InvalidOperationException("Response deserialized to null."),
                        safePath);
                }

                return result;
            }
            catch (JsonException exception)
            {
                throw TwitchErrorMapper.FromDeserializationError(exception, safePath);
            }
        }
    }

    /// <summary>
    /// Sends a request that expects no response body (e.g., 204 No Content).
    /// </summary>
    public async Task SendAsync(
        HttpMethod method,
        string endpoint,
        TwitchAuthenticationMode authMode,
        HttpContent? content = null,
        CancellationToken cancellationToken = default)
    {
        var safePath = LogRedaction.GetSafePath(endpoint);

        using var lease = await _rateLimiter.AcquireAsync(safePath, cancellationToken).ConfigureAwait(false);
        using var request = CreateRequest(method, endpoint, authMode, content);

        var client = _helixClientFactory();

        LogRequestDispatched(method.Method, safePath);
        HttpResponseMessage response;
        try
        {
            response = await client.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken).ConfigureAwait(false);
        }
        catch (HttpRequestException exception)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, safePath);
        }
        catch (TaskCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, safePath);
        }

        using (response)
        {
            if (!response.IsSuccessStatusCode)
            {
                await HandleErrorResponseAsync(response, safePath, cancellationToken).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Sends a request and returns the response body as a raw string.
    /// Used for non-JSON endpoints like iCalendar.
    /// </summary>
    public async Task<string> SendRawAsync(
        HttpMethod method,
        string endpoint,
        TwitchAuthenticationMode authMode,
        HttpContent? content = null,
        CancellationToken cancellationToken = default)
    {
        var safePath = LogRedaction.GetSafePath(endpoint);

        using var lease = await _rateLimiter.AcquireAsync(safePath, cancellationToken).ConfigureAwait(false);
        using var request = CreateRequest(method, endpoint, authMode, content);

        var client = _helixClientFactory();

        LogRequestDispatched(method.Method, safePath);
        HttpResponseMessage response;
        try
        {
            response = await client.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken).ConfigureAwait(false);
        }
        catch (HttpRequestException exception)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, safePath);
        }
        catch (TaskCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            throw TwitchErrorMapper.FromNetworkException(exception, safePath);
        }

        using (response)
        {
            if (!response.IsSuccessStatusCode)
            {
                await HandleErrorResponseAsync(response, safePath, cancellationToken).ConfigureAwait(false);
            }

            return await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    private static HttpRequestMessage CreateRequest(
        HttpMethod method,
        string endpoint,
        TwitchAuthenticationMode authMode,
        HttpContent? content)
    {
        var request = new HttpRequestMessage(method, endpoint);
        request.Options.Set(TwitchRequestOptions.AuthenticationMode, authMode);

        if (content is not null)
        {
            request.Content = content;
        }

        return request;
    }

    private async Task HandleErrorResponseAsync(
        HttpResponseMessage response,
        string endpoint,
        CancellationToken cancellationToken)
    {
        TimeSpan? retryAfter = null;

        if (response.StatusCode == HttpStatusCode.TooManyRequests)
        {
            retryAfter = GetRetryAfterFromResponse(response);
        }

        string? errorMessage = null;
        try
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(errorContent))
            {
                try
                {
                    var errorResponse = JsonSerializer.Deserialize(
                        errorContent,
                        TwitchApiJsonContext.Default.TwitchErrorResponse);
                    errorMessage = errorResponse?.Message;
                }
                catch (JsonException)
                {
                    errorMessage = errorContent.Length > 200
                        ? string.Concat(errorContent.AsSpan(0, 200), "...")
                        : errorContent;
                }
            }
        }
        catch (HttpRequestException)
        {
            // Ignore content reading errors
        }
        catch (IOException)
        {
            // Ignore content reading errors
        }

        LogErrorResponse(endpoint, (int)response.StatusCode, errorMessage);
        throw TwitchErrorMapper.FromHttpResponse(response.StatusCode, endpoint, errorMessage, retryAfter);
    }

    private static TimeSpan? GetRetryAfterFromResponse(HttpResponseMessage response)
    {
        if (response.Headers.RetryAfter is { } retryAfter)
        {
            if (retryAfter.Delta is { } delta)
            {
                return ClampRetryAfter(delta);
            }

            if (retryAfter.Date is { } date)
            {
                var delay = date - DateTimeOffset.UtcNow;
                return ClampRetryAfter(delay);
            }
        }

        if (response.Headers.TryGetValues("Ratelimit-Reset", out var resetValues))
        {
            var resetValue = resetValues.FirstOrDefault();
            if (long.TryParse(resetValue, out var resetTimestamp))
            {
                var resetTime = DateTimeOffset.FromUnixTimeSeconds(resetTimestamp);
                var delay = resetTime - DateTimeOffset.UtcNow;
                return ClampRetryAfter(delay);
            }
        }

        return null;
    }

    private static TimeSpan ClampRetryAfter(TimeSpan delay)
    {
        if (delay < TimeSpan.Zero) return TimeSpan.Zero;
        if (delay > TimeSpan.FromMinutes(2)) return TimeSpan.FromMinutes(2);
        return delay;
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        await _rateLimiter.DisposeAsync().ConfigureAwait(false);
    }
}
