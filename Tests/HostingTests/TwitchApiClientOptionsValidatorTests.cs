using TwitchSharp.Api;
using TwitchSharp.Hosting;

namespace HostingTests;

public sealed class TwitchApiClientOptionsValidatorTests
{
    private readonly TwitchApiClientOptionsValidator _validator = new();

    [Fact]
    public void Validate_ValidOptions_ReturnsSuccess()
    {
        var options = new TwitchApiClientOptions
        {
            ClientId = "valid_client_id",
            MaxRetryAttempts = 3,
            RateLimitQueueLimit = 512,
            RequestTimeout = TimeSpan.FromSeconds(30),
            TokenExpirationBuffer = TimeSpan.FromSeconds(60)
        };

        var result = _validator.Validate(null, options);

        Assert.True(result.Succeeded);
    }

    [Fact]
    public void Validate_EmptyClientId_ReturnsFail()
    {
        var options = new TwitchApiClientOptions
        {
            ClientId = string.Empty
        };

        var result = _validator.Validate(null, options);

        Assert.True(result.Failed);
        Assert.Contains(result.Failures, failure => failure.Contains("ClientId"));
    }

    [Fact]
    public void Validate_WhitespaceClientId_ReturnsFail()
    {
        var options = new TwitchApiClientOptions
        {
            ClientId = "   "
        };

        var result = _validator.Validate(null, options);

        Assert.True(result.Failed);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    [InlineData(-100)]
    [InlineData(50)]
    public void Validate_InvalidMaxRetryAttempts_ReturnsFail(int maxRetryAttempts)
    {
        var options = new TwitchApiClientOptions
        {
            ClientId = "valid_client_id",
            MaxRetryAttempts = maxRetryAttempts
        };

        var result = _validator.Validate(null, options);

        Assert.True(result.Failed);
        Assert.Contains(result.Failures, failure => failure.Contains("MaxRetryAttempts"));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public void Validate_ValidMaxRetryAttempts_DoesNotFail(int maxRetryAttempts)
    {
        var options = new TwitchApiClientOptions
        {
            ClientId = "valid_client_id",
            MaxRetryAttempts = maxRetryAttempts
        };

        var result = _validator.Validate(null, options);

        Assert.True(result.Succeeded);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(10_001)]
    [InlineData(100_000)]
    public void Validate_InvalidRateLimitQueueLimit_ReturnsFail(int queueLimit)
    {
        var options = new TwitchApiClientOptions
        {
            ClientId = "valid_client_id",
            RateLimitQueueLimit = queueLimit
        };

        var result = _validator.Validate(null, options);

        Assert.True(result.Failed);
        Assert.Contains(result.Failures, failure => failure.Contains("RateLimitQueueLimit"));
    }

    [Fact]
    public void Validate_ZeroRequestTimeout_ReturnsFail()
    {
        var options = new TwitchApiClientOptions
        {
            ClientId = "valid_client_id",
            RequestTimeout = TimeSpan.Zero
        };

        var result = _validator.Validate(null, options);

        Assert.True(result.Failed);
        Assert.Contains(result.Failures, failure => failure.Contains("RequestTimeout"));
    }

    [Fact]
    public void Validate_NegativeRequestTimeout_ReturnsFail()
    {
        var options = new TwitchApiClientOptions
        {
            ClientId = "valid_client_id",
            RequestTimeout = TimeSpan.FromSeconds(-1)
        };

        var result = _validator.Validate(null, options);

        Assert.True(result.Failed);
    }

    [Fact]
    public void Validate_NegativeTokenExpirationBuffer_ReturnsFail()
    {
        var options = new TwitchApiClientOptions
        {
            ClientId = "valid_client_id",
            TokenExpirationBuffer = TimeSpan.FromSeconds(-1)
        };

        var result = _validator.Validate(null, options);

        Assert.True(result.Failed);
        Assert.Contains(result.Failures, failure => failure.Contains("TokenExpirationBuffer"));
    }

    [Fact]
    public void Validate_ZeroTokenExpirationBuffer_ReturnsSuccess()
    {
        var options = new TwitchApiClientOptions
        {
            ClientId = "valid_client_id",
            TokenExpirationBuffer = TimeSpan.Zero
        };

        var result = _validator.Validate(null, options);

        Assert.True(result.Succeeded);
    }

    [Fact]
    public void Validate_MultipleFailures_ReturnsAllFailures()
    {
        var options = new TwitchApiClientOptions
        {
            ClientId = string.Empty,
            MaxRetryAttempts = -1,
            RateLimitQueueLimit = 0,
            RequestTimeout = TimeSpan.Zero,
            TokenExpirationBuffer = TimeSpan.FromSeconds(-1)
        };

        var result = _validator.Validate(null, options);

        Assert.True(result.Failed);
        var failures = result.Failures.ToList();
        Assert.True(failures.Count >= 5, $"Expected at least 5 failures but got {failures.Count}");
    }
}
