using TwitchSharp.Api;
using TwitchSharp.Api.Authentication;
using TwitchSharp.Extensions.Authentication;

namespace AuthenticationExtensionsTests;

public sealed class InteractiveAuthenticationFlowTests : IAsyncDisposable
{
    private readonly string _testTokenFilePath;

    public InteractiveAuthenticationFlowTests()
    {
        _testTokenFilePath = Path.Combine(Path.GetTempPath(), $"twitchsharp_flow_test_{Guid.NewGuid():N}.json");
    }

    [Fact]
    public async Task AuthenticateAsync_ReturnsCachedTokenWhenNotExpired()
    {
        // Pre-save a valid token to the file
        var tokenStore = new FileTokenStore(_testTokenFilePath);
        var cachedToken = new TwitchTokenSet
        {
            AccessToken = "cached_token",
            RefreshToken = "cached_refresh",
            ExpiresAtUtc = DateTimeOffset.UtcNow.AddHours(1),
            Scopes = ["chat:read"]
        };
        await tokenStore.SaveAsync(cachedToken, TestContext.Current.CancellationToken);

        var flow = new InteractiveAuthenticationFlow(new InteractiveAuthenticationFlowOptions
        {
            ClientId = "test_client_id",
            RedirectUri = "http://localhost:19890/callback/",
            TokenFilePath = _testTokenFilePath
        });

        // Create a real client that will never be used (cached path returns early)
        await using var client = TwitchApiClient.Create(new TwitchApiClientOptions
        {
            ClientId = "test_client_id"
        });

        var result = await flow.AuthenticateAsync(client, ["chat:read"], TestContext.Current.CancellationToken);

        Assert.Equal("cached_token", result.AccessToken);
        Assert.Equal("cached_refresh", result.RefreshToken);
    }

    [Fact]
    public void Constructor_AcceptsValidOptions()
    {
        var flow = new InteractiveAuthenticationFlow(new InteractiveAuthenticationFlowOptions
        {
            ClientId = "test_client_id",
            RedirectUri = "http://localhost:19892/callback/"
        });

        Assert.NotNull(flow);
    }

    public ValueTask DisposeAsync()
    {
        if (File.Exists(_testTokenFilePath))
        {
            File.Delete(_testTokenFilePath);
        }
        return ValueTask.CompletedTask;
    }
}
