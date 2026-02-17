using TwitchSharp.Extensions.Authentication;

namespace AuthenticationExtensionsTests;

public sealed class StateGeneratorTests
{
    [Fact]
    public void Generate_ReturnsNonEmptyString()
    {
        var state = StateGenerator.Generate();

        Assert.NotNull(state);
        Assert.NotEmpty(state);
    }

    [Fact]
    public void Generate_Returns43CharacterString()
    {
        var state = StateGenerator.Generate();

        Assert.Equal(43, state.Length);
    }

    [Fact]
    public void Generate_ReturnsUrlSafeCharacters()
    {
        var state = StateGenerator.Generate();

        Assert.DoesNotContain("+", state);
        Assert.DoesNotContain("/", state);
        Assert.DoesNotContain("=", state);
    }

    [Fact]
    public void Generate_ProducesDifferentValuesOnEachCall()
    {
        var firstState = StateGenerator.Generate();
        var secondState = StateGenerator.Generate();

        Assert.NotEqual(firstState, secondState);
    }
}
