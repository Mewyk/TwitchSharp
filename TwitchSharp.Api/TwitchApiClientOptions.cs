using System.ComponentModel.DataAnnotations;

namespace TwitchSharp.Api;

/// <summary>
/// Configuration options for <see cref="TwitchApiClient"/>.
/// </summary>
public sealed class TwitchApiClientOptions
{
    /// <summary>
    /// The Twitch application Client ID. Required.
    /// </summary>
    [Required(ErrorMessage = "ClientId is required for Twitch API access.")]
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// The Twitch application Client Secret. Required for client credentials and authorization code flows.
    /// </summary>
    public string? ClientSecret { get; set; }

    /// <summary>
    /// The timeout for individual HTTP requests. Default is 30 seconds.
    /// </summary>
    public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// The maximum number of automatic retry attempts for transient failures. Default is 2.
    /// </summary>
    [Range(0, 10)]
    public int MaxRetryAttempts { get; set; } = 2;

    /// <summary>
    /// The maximum number of requests that can be queued when the rate limiter is full. Default is 1024.
    /// </summary>
    [Range(1, 10_000)]
    public int RateLimitQueueLimit { get; set; } = 1_024;

    /// <summary>
    /// Set to <c>true</c> to disable the built-in resilience handler (retry logic).
    /// </summary>
    public bool DisableBuiltInResilience { get; set; }

    /// <summary>
    /// Time before actual expiration to consider a token expired, allowing proactive refresh.
    /// Default is 60 seconds.
    /// </summary>
    public TimeSpan TokenExpirationBuffer { get; set; } = TimeSpan.FromSeconds(60);
}
