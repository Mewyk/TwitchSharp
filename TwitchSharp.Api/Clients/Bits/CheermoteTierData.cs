using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a tier level of a Cheermote.
/// </summary>
public sealed record CheermoteTierData
{
    /// <summary>The minimum number of Bits for this tier.</summary>
    [JsonPropertyName("min_bits")]
    public int MinBits { get; init; }

    /// <summary>The tier level ID (e.g., "1", "100", "500", "1000", "5000", "10000", "100000").</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The hex color code for this tier level.</summary>
    [JsonPropertyName("color")]
    public string Color { get; init; } = string.Empty;

    /// <summary>The image sets organized by theme (dark/light).</summary>
    [JsonPropertyName("images")]
    public CheermoteImagesData? Images { get; init; }

    /// <summary>Whether users can cheer at this tier level.</summary>
    [JsonPropertyName("can_cheer")]
    public bool CanCheer { get; init; }

    /// <summary>Whether this tier is shown in the Bits card.</summary>
    [JsonPropertyName("show_in_bits_card")]
    public bool ShowInBitsCard { get; init; }
}
