using TwitchSharp.Api.Http;

namespace ApiTests;

public sealed class HelixUrlBuilderTests
{
    [Fact]
    public void Build_PathOnly_ReturnsPath()
    {
        var builder = new HelixUrlBuilder("helix/users");

        var result = builder.Build();

        Assert.Equal("helix/users", result);
    }

    [Fact]
    public void Add_StringValue_AppendsQueryParam()
    {
        var builder = new HelixUrlBuilder("helix/users");
        builder.Add("login", "testuser");

        var result = builder.Build();

        Assert.Equal("helix/users?login=testuser", result);
    }

    [Fact]
    public void Add_NullStringValue_SkipsParam()
    {
        var builder = new HelixUrlBuilder("helix/users");
        builder.Add("login", (string?)null);

        var result = builder.Build();

        Assert.Equal("helix/users", result);
    }

    [Fact]
    public void Add_IntValue_AppendsQueryParam()
    {
        var builder = new HelixUrlBuilder("helix/streams");
        builder.Add("first", 20);

        var result = builder.Build();

        Assert.Equal("helix/streams?first=20", result);
    }

    [Fact]
    public void Add_NullIntValue_SkipsParam()
    {
        var builder = new HelixUrlBuilder("helix/streams");
        builder.Add("first", (int?)null);

        var result = builder.Build();

        Assert.Equal("helix/streams", result);
    }

    [Fact]
    public void Add_BoolTrue_AppendsTrueString()
    {
        var builder = new HelixUrlBuilder("helix/chat/settings");
        builder.Add("emote_mode", true);

        var result = builder.Build();

        Assert.Equal("helix/chat/settings?emote_mode=true", result);
    }

    [Fact]
    public void Add_BoolFalse_AppendsFalseString()
    {
        var builder = new HelixUrlBuilder("helix/chat/settings");
        builder.Add("emote_mode", false);

        var result = builder.Build();

        Assert.Equal("helix/chat/settings?emote_mode=false", result);
    }

    [Fact]
    public void Add_NullBoolValue_SkipsParam()
    {
        var builder = new HelixUrlBuilder("helix/chat/settings");
        builder.Add("emote_mode", (bool?)null);

        var result = builder.Build();

        Assert.Equal("helix/chat/settings", result);
    }

    [Fact]
    public void Add_DoubleValue_AppendsQueryParam()
    {
        var builder = new HelixUrlBuilder("helix/test");
        builder.Add("amount", 1.5);

        var result = builder.Build();

        Assert.Equal("helix/test?amount=1.5", result);
    }

    [Fact]
    public void Add_NullDoubleValue_SkipsParam()
    {
        var builder = new HelixUrlBuilder("helix/test");
        builder.Add("amount", (double?)null);

        var result = builder.Build();

        Assert.Equal("helix/test", result);
    }

    [Fact]
    public void Add_MultipleParams_JoinsWithAmpersand()
    {
        var builder = new HelixUrlBuilder("helix/streams");
        builder.Add("game_id", "12345");
        builder.Add("first", 10);

        var result = builder.Build();

        Assert.Equal("helix/streams?game_id=12345&first=10", result);
    }

    [Fact]
    public void Add_SpecialCharacters_EscapesValue()
    {
        var builder = new HelixUrlBuilder("helix/users");
        builder.Add("login", "test user&special=chars");

        var result = builder.Build();

        Assert.Contains("test%20user%26special%3Dchars", result);
    }

    [Fact]
    public void AddRepeated_MultipleValues_AddsEachWithSameKey()
    {
        var builder = new HelixUrlBuilder("helix/users");
        builder.AddRepeated("id", ["123", "456", "789"]);

        var result = builder.Build();

        Assert.Equal("helix/users?id=123&id=456&id=789", result);
    }

    [Fact]
    public void AddRepeated_NullValues_SkipsAll()
    {
        var builder = new HelixUrlBuilder("helix/users");
        builder.AddRepeated("id", null);

        var result = builder.Build();

        Assert.Equal("helix/users", result);
    }

    [Fact]
    public void AddRepeated_EmptyCollection_ProducesNoParams()
    {
        var builder = new HelixUrlBuilder("helix/users");
        builder.AddRepeated("id", []);

        var result = builder.Build();

        Assert.Equal("helix/users", result);
    }

    [Fact]
    public void Add_MixedParamsAndRepeated_BuildsCorrectly()
    {
        var builder = new HelixUrlBuilder("helix/streams");
        builder.Add("first", 10);
        builder.AddRepeated("user_id", ["111", "222"]);
        builder.Add("type", "live");

        var result = builder.Build();

        Assert.Equal("helix/streams?first=10&user_id=111&user_id=222&type=live", result);
    }
}
