using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the themed image sets for a Cheermote tier (dark and light themes).
/// </summary>
public sealed record CheermoteImagesData
{
    /// <summary>The dark theme image set.</summary>
    [JsonPropertyName("dark")]
    public CheermoteThemeData? Dark { get; init; }

    /// <summary>The light theme image set.</summary>
    [JsonPropertyName("light")]
    public CheermoteThemeData? Light { get; init; }
}
