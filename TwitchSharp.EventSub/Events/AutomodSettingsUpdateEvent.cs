using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents an automod.settings.update v1 event, fired when AutoMod settings are updated.</summary>
public sealed record AutomodSettingsUpdateEvent
{
    /// <summary>The broadcaster's user ID.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's user login.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's user display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The moderator's user ID who updated the settings.</summary>
    [JsonPropertyName("moderator_user_id")]
    public string ModeratorUserId { get; init; } = string.Empty;

    /// <summary>The moderator's user login who updated the settings.</summary>
    [JsonPropertyName("moderator_user_login")]
    public string ModeratorUserLogin { get; init; } = string.Empty;

    /// <summary>The moderator's user display name who updated the settings.</summary>
    [JsonPropertyName("moderator_user_name")]
    public string ModeratorUserName { get; init; } = string.Empty;

    /// <summary>The default AutoMod level for the broadcaster, or null if individual settings are used.</summary>
    [JsonPropertyName("overall_level")]
    public int? OverallLevel { get; init; }

    /// <summary>The AutoMod level for hostility involving aggression.</summary>
    [JsonPropertyName("aggression")]
    public int Aggression { get; init; }

    /// <summary>The AutoMod level for hostility involving name calling or insults.</summary>
    [JsonPropertyName("bullying")]
    public int Bullying { get; init; }

    /// <summary>The AutoMod level for discrimination against disability.</summary>
    [JsonPropertyName("disability")]
    public int Disability { get; init; }

    /// <summary>The AutoMod level for discrimination against women.</summary>
    [JsonPropertyName("misogyny")]
    public int Misogyny { get; init; }

    /// <summary>The AutoMod level for racial discrimination.</summary>
    [JsonPropertyName("race_ethnicity_or_religion")]
    public int RaceEthnicityOrReligion { get; init; }

    /// <summary>The AutoMod level for sexual content.</summary>
    [JsonPropertyName("sex_based_terms")]
    public int SexBasedTerms { get; init; }

    /// <summary>The AutoMod level for discrimination based on sexuality, sex, or gender.</summary>
    [JsonPropertyName("sexuality_sex_or_gender")]
    public int SexualitySexOrGender { get; init; }

    /// <summary>The AutoMod level for profanity.</summary>
    [JsonPropertyName("swearing")]
    public int Swearing { get; init; }
}
