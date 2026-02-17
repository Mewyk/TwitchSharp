using TwitchSharp.Api.Authentication;
using TwitchSharp.Extensions.Authentication;

namespace AuthenticationExtensionsTests;

public sealed class FileTokenStoreTests : IDisposable
{
    private readonly string _testFilePath;

    public FileTokenStoreTests()
    {
        _testFilePath = Path.Combine(Path.GetTempPath(), $"twitchsharp_test_{Guid.NewGuid():N}.json");
    }

    [Fact]
    public async Task SaveAsync_AndLoadAsync_RoundTripsAllProperties()
    {
        var store = new FileTokenStore(_testFilePath);
        var originalToken = new TwitchTokenSet
        {
            AccessToken = "test_access_token",
            RefreshToken = "test_refresh_token",
            ExpiresAtUtc = new DateTimeOffset(2026, 6, 15, 12, 0, 0, TimeSpan.Zero),
            TokenType = "bearer",
            Scopes = ["chat:read", "chat:edit", "channel:moderate"],
            IdToken = "test_id_token_jwt"
        };

        await store.SaveAsync(originalToken, TestContext.Current.CancellationToken);
        var loadedToken = await store.LoadAsync(TestContext.Current.CancellationToken);

        Assert.NotNull(loadedToken);
        Assert.Equal(originalToken.AccessToken, loadedToken.AccessToken);
        Assert.Equal(originalToken.RefreshToken, loadedToken.RefreshToken);
        Assert.Equal(originalToken.ExpiresAtUtc, loadedToken.ExpiresAtUtc);
        Assert.Equal(originalToken.TokenType, loadedToken.TokenType);
        Assert.Equal(originalToken.Scopes, loadedToken.Scopes);
        Assert.Equal(originalToken.IdToken, loadedToken.IdToken);
    }

    [Fact]
    public async Task SaveAsync_AndLoadAsync_RoundTripsMinimalToken()
    {
        var store = new FileTokenStore(_testFilePath);
        var originalToken = new TwitchTokenSet
        {
            AccessToken = "minimal_token"
        };

        await store.SaveAsync(originalToken, TestContext.Current.CancellationToken);
        var loadedToken = await store.LoadAsync(TestContext.Current.CancellationToken);

        Assert.NotNull(loadedToken);
        Assert.Equal("minimal_token", loadedToken.AccessToken);
        Assert.Null(loadedToken.RefreshToken);
        Assert.Null(loadedToken.ExpiresAtUtc);
        Assert.Equal("bearer", loadedToken.TokenType);
        Assert.Empty(loadedToken.Scopes);
        Assert.Null(loadedToken.IdToken);
    }

    [Fact]
    public async Task LoadAsync_ReturnsNullWhenFileDoesNotExist()
    {
        var store = new FileTokenStore(_testFilePath);

        var result = await store.LoadAsync(TestContext.Current.CancellationToken);

        Assert.Null(result);
    }

    [Fact]
    public async Task Delete_ReturnsTrueWhenFileExists()
    {
        var store = new FileTokenStore(_testFilePath);
        await store.SaveAsync(
            new TwitchTokenSet { AccessToken = "token" },
            TestContext.Current.CancellationToken);

        var result = store.Delete();

        Assert.True(result);
        Assert.False(File.Exists(_testFilePath));
    }

    [Fact]
    public void Delete_ReturnsFalseWhenFileDoesNotExist()
    {
        var store = new FileTokenStore(_testFilePath);

        var result = store.Delete();

        Assert.False(result);
    }

    [Fact]
    public async Task CreatePersistenceCallback_SavesTokenWhenInvoked()
    {
        var store = new FileTokenStore(_testFilePath);
        var callback = store.CreatePersistenceCallback();
        var tokenSet = new TwitchTokenSet
        {
            AccessToken = "callback_token",
            RefreshToken = "callback_refresh"
        };

        await callback(tokenSet, TestContext.Current.CancellationToken);
        var loadedToken = await store.LoadAsync(TestContext.Current.CancellationToken);

        Assert.NotNull(loadedToken);
        Assert.Equal("callback_token", loadedToken.AccessToken);
        Assert.Equal("callback_refresh", loadedToken.RefreshToken);
    }

    [Fact]
    public async Task SaveAsync_OverwritesExistingFile()
    {
        var store = new FileTokenStore(_testFilePath);

        await store.SaveAsync(
            new TwitchTokenSet { AccessToken = "first_token" },
            TestContext.Current.CancellationToken);
        await store.SaveAsync(
            new TwitchTokenSet { AccessToken = "second_token" },
            TestContext.Current.CancellationToken);

        var loadedToken = await store.LoadAsync(TestContext.Current.CancellationToken);

        Assert.NotNull(loadedToken);
        Assert.Equal("second_token", loadedToken.AccessToken);
    }

    public void Dispose()
    {
        if (File.Exists(_testFilePath))
        {
            File.Delete(_testFilePath);
        }
    }
}
