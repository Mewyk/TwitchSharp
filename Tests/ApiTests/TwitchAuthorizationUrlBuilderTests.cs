using TwitchSharp.Api.Authentication;

namespace ApiTests;

public sealed class TwitchAuthorizationUrlBuilderTests
{
    [Fact]
    public void Build_MinimalConfig_ContainsRequiredParams()
    {
        var url = TwitchAuthorizationUrlBuilder.Create()
            .WithClientId("test_client_id")
            .WithRedirectUri("https://localhost/callback")
            .Build();

        var urlString = url.ToString();
        Assert.Contains("client_id=test_client_id", urlString);
        Assert.Contains("redirect_uri=", urlString);
        Assert.Contains("response_type=code", urlString);
        Assert.StartsWith("https://id.twitch.tv/oauth2/authorize?", urlString);
    }

    [Fact]
    public void Build_MissingClientId_ThrowsInvalidOperationException()
    {
        var builder = TwitchAuthorizationUrlBuilder.Create()
            .WithRedirectUri("https://localhost/callback");

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void Build_MissingRedirectUri_ThrowsInvalidOperationException()
    {
        var builder = TwitchAuthorizationUrlBuilder.Create()
            .WithClientId("test_client_id");

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void Build_WithScopes_ContainsScopeParam()
    {
        var url = TwitchAuthorizationUrlBuilder.Create()
            .WithClientId("test_client_id")
            .WithRedirectUri("https://localhost/callback")
            .WithScopes("channel:read:subscriptions", "user:read:email")
            .Build();

        var urlString = url.ToString();
        Assert.Contains("scope=", urlString);
    }

    [Fact]
    public void Build_ForImplicitGrant_SetsResponseTypeToken()
    {
        var url = TwitchAuthorizationUrlBuilder.Create()
            .WithClientId("test_client_id")
            .WithRedirectUri("https://localhost/callback")
            .ForImplicitGrant()
            .Build();

        Assert.Contains("response_type=token", url.ToString());
    }

    [Fact]
    public void Build_ForAuthorizationCode_SetsResponseTypeCode()
    {
        var url = TwitchAuthorizationUrlBuilder.Create()
            .WithClientId("test_client_id")
            .WithRedirectUri("https://localhost/callback")
            .ForAuthorizationCode()
            .Build();

        Assert.Contains("response_type=code", url.ToString());
    }

    [Fact]
    public void Build_WithState_ContainsStateParam()
    {
        var url = TwitchAuthorizationUrlBuilder.Create()
            .WithClientId("test_client_id")
            .WithRedirectUri("https://localhost/callback")
            .WithState("csrf_token_value")
            .Build();

        Assert.Contains("state=csrf_token_value", url.ToString());
    }

    [Fact]
    public void Build_ForceVerify_ContainsForceVerifyTrue()
    {
        var url = TwitchAuthorizationUrlBuilder.Create()
            .WithClientId("test_client_id")
            .WithRedirectUri("https://localhost/callback")
            .ForceVerify()
            .Build();

        Assert.Contains("force_verify=true", url.ToString());
    }

    [Fact]
    public void Build_WithPkce_ContainsChallengeAndMethod()
    {
        var url = TwitchAuthorizationUrlBuilder.Create()
            .WithClientId("test_client_id")
            .WithRedirectUri("https://localhost/callback")
            .WithPkce("test_challenge_value")
            .Build();

        var urlString = url.ToString();
        Assert.Contains("code_challenge=test_challenge_value", urlString);
        Assert.Contains("code_challenge_method=S256", urlString);
    }

    [Fact]
    public void Build_ForOidcAuthorizationCode_AddsOpenIdScope()
    {
        var url = TwitchAuthorizationUrlBuilder.Create()
            .WithClientId("test_client_id")
            .WithRedirectUri("https://localhost/callback")
            .ForOidcAuthorizationCode()
            .Build();

        var urlString = url.ToString();
        Assert.Contains("response_type=code", urlString);
        Assert.Contains("scope=", urlString);
        Assert.Contains("openid", Uri.UnescapeDataString(urlString));
    }

    [Fact]
    public void Build_WithNonce_ContainsNonceParam()
    {
        var url = TwitchAuthorizationUrlBuilder.Create()
            .WithClientId("test_client_id")
            .WithRedirectUri("https://localhost/callback")
            .WithNonce("unique_nonce_value")
            .Build();

        Assert.Contains("nonce=unique_nonce_value", url.ToString());
    }

    [Fact]
    public void Build_WithClaims_ContainsClaimsParam()
    {
        var claimsJson = """{"id_token":{"email":null}}""";

        var url = TwitchAuthorizationUrlBuilder.Create()
            .WithClientId("test_client_id")
            .WithRedirectUri("https://localhost/callback")
            .WithClaims(claimsJson)
            .Build();

        Assert.Contains("claims=", url.ToString());
    }

    [Fact]
    public void WithClientId_NullValue_ThrowsArgumentNullException()
    {
#nullable disable
        string nullClientId = null;
        Assert.Throws<ArgumentNullException>(() =>
            TwitchAuthorizationUrlBuilder.Create().WithClientId(nullClientId));
#nullable restore
    }

    [Fact]
    public void WithRedirectUri_NullValue_ThrowsArgumentNullException()
    {
#nullable disable
        string nullRedirectUri = null;
        Assert.Throws<ArgumentNullException>(() =>
            TwitchAuthorizationUrlBuilder.Create().WithRedirectUri(nullRedirectUri));
#nullable restore
    }
}
