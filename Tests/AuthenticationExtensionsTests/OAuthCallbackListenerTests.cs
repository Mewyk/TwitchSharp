using TwitchSharp.Extensions.Authentication;

namespace AuthenticationExtensionsTests;

public sealed class OAuthCallbackListenerTests
{
    [Fact]
    public void Constructor_CreatesListenerWithoutThrowing()
    {
        using var listener = new OAuthCallbackListener("http://localhost:19876/callback/");
    }

    [Fact]
    public void Constructor_AppendsTrailingSlashIfMissing()
    {
        // Should not throw -- HttpListener requires trailing slash, constructor normalizes it
        using var listener = new OAuthCallbackListener("http://localhost:19877/callback");
    }

    [Fact]
    public async Task ListenForCallbackAsync_ExtractsCodeAndState()
    {
        using var listener = new OAuthCallbackListener("http://localhost:19878/callback/");

        var listenTask = listener.ListenForCallbackAsync(TestContext.Current.CancellationToken);

        // Simulate the OAuth callback by sending an HTTP request to the listener
        using var httpClient = new HttpClient();
        await httpClient.GetAsync(
            "http://localhost:19878/callback/?code=test_code_123&state=test_state_456",
            TestContext.Current.CancellationToken);

        var result = await listenTask;

        Assert.Equal("test_code_123", result.Code);
        Assert.Equal("test_state_456", result.State);
    }

    [Fact]
    public async Task ListenForCallbackAsync_ReturnsNullStateWhenNotPresent()
    {
        using var listener = new OAuthCallbackListener("http://localhost:19879/callback/");

        var listenTask = listener.ListenForCallbackAsync(TestContext.Current.CancellationToken);

        using var httpClient = new HttpClient();
        await httpClient.GetAsync(
            "http://localhost:19879/callback/?code=test_code_only",
            TestContext.Current.CancellationToken);

        var result = await listenTask;

        Assert.Equal("test_code_only", result.Code);
        Assert.Null(result.State);
    }

    [Fact]
    public async Task ListenForCallbackAsync_ThrowsWhenCodeIsMissing()
    {
        using var listener = new OAuthCallbackListener("http://localhost:19880/callback/");

        var listenTask = listener.ListenForCallbackAsync(TestContext.Current.CancellationToken);

        using var httpClient = new HttpClient();
        try
        {
            await httpClient.GetAsync(
                "http://localhost:19880/callback/?state=only_state",
                TestContext.Current.CancellationToken);
        }
        catch (HttpRequestException)
        {
            // The listener may close the connection; ignore client-side errors
        }

        await Assert.ThrowsAsync<InvalidOperationException>(() => listenTask);
    }

    [Fact]
    public async Task ListenForCallbackAsync_ThrowsOperationCanceledWhenCancelled()
    {
        using var listener = new OAuthCallbackListener("http://localhost:19881/callback/");
        using var cancellationTokenSource = new CancellationTokenSource();

        var listenTask = listener.ListenForCallbackAsync(cancellationTokenSource.Token);

        // Give the listener a moment to start, then cancel
        await Task.Delay(50, TestContext.Current.CancellationToken);
        await cancellationTokenSource.CancelAsync();

        await Assert.ThrowsAsync<OperationCanceledException>(() => listenTask);
    }

    [Fact]
    public async Task ListenForCallbackAsync_ServesSuccessHtml()
    {
        using var listener = new OAuthCallbackListener("http://localhost:19882/callback/");

        var listenTask = listener.ListenForCallbackAsync(TestContext.Current.CancellationToken);

        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(
            "http://localhost:19882/callback/?code=test_code",
            TestContext.Current.CancellationToken);
        var content = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Authorization Complete", content);

        await listenTask;
    }

    [Fact]
    public async Task ListenForCallbackAsync_ServesCustomSuccessHtml()
    {
        var options = new OAuthCallbackListenerOptions
        {
            SuccessHtml = "<html><body>Custom success page</body></html>"
        };
        using var listener = new OAuthCallbackListener("http://localhost:19883/callback/", options);

        var listenTask = listener.ListenForCallbackAsync(TestContext.Current.CancellationToken);

        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(
            "http://localhost:19883/callback/?code=test_code",
            TestContext.Current.CancellationToken);
        var content = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

        Assert.Contains("Custom success page", content);

        await listenTask;
    }

    [Fact]
    public void Dispose_CanBeCalledMultipleTimes()
    {
        var listener = new OAuthCallbackListener("http://localhost:19884/callback/");
        listener.Dispose();
        listener.Dispose();
    }
}
