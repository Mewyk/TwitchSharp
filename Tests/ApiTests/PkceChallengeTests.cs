using TwitchSharp.Api.Authentication;

namespace ApiTests;

public sealed class PkceChallengeTests
{
    [Fact]
    public void Generate_ReturnsNonEmptyVerifierAndChallenge()
    {
        var (codeVerifier, codeChallenge) = PkceChallenge.Generate();

        Assert.False(string.IsNullOrEmpty(codeVerifier));
        Assert.False(string.IsNullOrEmpty(codeChallenge));
    }

    [Fact]
    public void Generate_VerifierIs43Characters()
    {
        var (codeVerifier, _) = PkceChallenge.Generate();

        Assert.Equal(43, codeVerifier.Length);
    }

    [Fact]
    public void Generate_ChallengeIsBase64UrlEncoded()
    {
        var (_, codeChallenge) = PkceChallenge.Generate();

        Assert.DoesNotContain("+", codeChallenge);
        Assert.DoesNotContain("/", codeChallenge);
        Assert.DoesNotContain("=", codeChallenge);
    }

    [Fact]
    public void Generate_VerifierIsBase64UrlEncoded()
    {
        var (codeVerifier, _) = PkceChallenge.Generate();

        Assert.DoesNotContain("+", codeVerifier);
        Assert.DoesNotContain("/", codeVerifier);
        Assert.DoesNotContain("=", codeVerifier);
    }

    [Fact]
    public void Generate_MultipleCallsProduceDifferentValues()
    {
        var (verifier1, challenge1) = PkceChallenge.Generate();
        var (verifier2, challenge2) = PkceChallenge.Generate();

        Assert.NotEqual(verifier1, verifier2);
        Assert.NotEqual(challenge1, challenge2);
    }

    [Fact]
    public void Generate_ChallengeIs43Characters()
    {
        var (_, codeChallenge) = PkceChallenge.Generate();

        Assert.Equal(43, codeChallenge.Length);
    }
}
