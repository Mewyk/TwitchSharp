using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Update AutoMod Settings endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record UpdateAutoModSettingsRequest
{
    /// <summary>The default AutoMod level (0-4). Mutually exclusive with individual settings.</summary>
    [JsonPropertyName("overall_level")]
    public int? OverallLevel { get; init; }

    /// <summary>The AutoMod level for hostility involving aggression (0-4).</summary>
    [JsonPropertyName("aggression")]
    public int? Aggression { get; init; }

    /// <summary>The AutoMod level for hostility involving name calling or insults (0-4).</summary>
    [JsonPropertyName("bullying")]
    public int? Bullying { get; init; }

    /// <summary>The AutoMod level for discrimination against disability (0-4).</summary>
    [JsonPropertyName("disability")]
    public int? Disability { get; init; }

    /// <summary>The AutoMod level for discrimination against women (0-4).</summary>
    [JsonPropertyName("misogyny")]
    public int? Misogyny { get; init; }

    /// <summary>The AutoMod level for racial discrimination (0-4).</summary>
    [JsonPropertyName("race_ethnicity_or_religion")]
    public int? RaceEthnicityOrReligion { get; init; }

    /// <summary>The AutoMod level for sexual content (0-4).</summary>
    [JsonPropertyName("sex_based_terms")]
    public int? SexBasedTerms { get; init; }

    /// <summary>The AutoMod level for discrimination based on sexuality, sex, or gender (0-4).</summary>
    [JsonPropertyName("sexuality_sex_or_gender")]
    public int? SexualitySexOrGender { get; init; }

    /// <summary>The AutoMod level for profanity (0-4).</summary>
    [JsonPropertyName("swearing")]
    public int? Swearing { get; init; }
}
