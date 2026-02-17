using Microsoft.Extensions.Options;
using TwitchSharp.Api;

namespace TwitchSharp.Hosting;

/// <summary>
/// Validates <see cref="TwitchApiClientOptions"/> at startup using <see cref="IValidateOptions{TOptions}"/>.
/// AOT-safe alternative to DataAnnotations validation.
/// </summary>
internal sealed class TwitchApiClientOptionsValidator : IValidateOptions<TwitchApiClientOptions>
{
    /// <inheritdoc />
    public ValidateOptionsResult Validate(string? name, TwitchApiClientOptions options)
    {
        var failures = new List<string>();

        if (string.IsNullOrWhiteSpace(options.ClientId))
        {
            failures.Add("ClientId is required for Twitch API access.");
        }

        if (options.MaxRetryAttempts is < 0 or > 10)
        {
            failures.Add("MaxRetryAttempts must be between 0 and 10.");
        }

        if (options.RateLimitQueueLimit is < 1 or > 10_000)
        {
            failures.Add("RateLimitQueueLimit must be between 1 and 10,000.");
        }

        if (options.RequestTimeout <= TimeSpan.Zero)
        {
            failures.Add("RequestTimeout must be positive.");
        }

        if (options.TokenExpirationBuffer < TimeSpan.Zero)
        {
            failures.Add("TokenExpirationBuffer must not be negative.");
        }

        return failures.Count > 0
            ? ValidateOptionsResult.Fail(failures)
            : ValidateOptionsResult.Success;
    }
}
