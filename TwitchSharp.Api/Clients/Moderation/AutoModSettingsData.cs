using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a broadcaster's AutoMod settings.
/// </summary>
public sealed record AutoModSettingsData
{
    /// <summary>The broadcaster's ID.</summary>
    [JsonPropertyName("broadcaster_id")]
    public string BroadcasterId { get; init; } = string.Empty;

    /// <summary>The moderator's ID.</summary>
    [JsonPropertyName("moderator_id")]
    public string ModeratorId { get; init; } = string.Empty;

    /// <summary>The default AutoMod level, or null if individual settings are used.</summary>
    [JsonPropertyName("overall_level")]
    public int? OverallLevel { get; init; }

    /// <summary>The AutoMod level for discrimination against disability.</summary>
    [JsonPropertyName("disability")]
    public int Disability { get; init; }

    /// <summary>The AutoMod level for hostility involving aggression.</summary>
    [JsonPropertyName("aggression")]
    public int Aggression { get; init; }

    /// <summary>The AutoMod level for discrimination based on sexuality, sex, or gender.</summary>
    [JsonPropertyName("sexuality_sex_or_gender")]
    public int SexualitySexOrGender { get; init; }

    /// <summary>The AutoMod level for discrimination against women.</summary>
    [JsonPropertyName("misogyny")]
    public int Misogyny { get; init; }

    /// <summary>The AutoMod level for hostility involving name calling or insults.</summary>
    [JsonPropertyName("bullying")]
    public int Bullying { get; init; }

    /// <summary>The AutoMod level for profanity.</summary>
    [JsonPropertyName("swearing")]
    public int Swearing { get; init; }

    /// <summary>The AutoMod level for racial discrimination.</summary>
    [JsonPropertyName("race_ethnicity_or_religion")]
    public int RaceEthnicityOrReligion { get; init; }

    /// <summary>The AutoMod level for sexual content.</summary>
    [JsonPropertyName("sex_based_terms")]
    public int SexBasedTerms { get; init; }
}
