using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a content classification label used in the Modify Channel Information request.
/// </summary>
[GenerateWithMethods]
public sealed partial record ContentClassificationLabel
{
    /// <summary>The label ID (e.g., DrugsIntoxication, SexualThemes, ViolentGraphic, Gambling, ProfanityVulgarity, DebatedSocialIssuesAndPolitics).</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>Whether this label is enabled.</summary>
    [JsonPropertyName("is_enabled")]
    public bool IsEnabled { get; init; }
}
