using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a Cheermote with its tiers and metadata.
/// </summary>
public sealed record CheermoteData
{
    /// <summary>The name portion of the Cheermote string used in chat (e.g., "Cheer" in "Cheer100").</summary>
    [JsonPropertyName("prefix")]
    public string Prefix { get; init; } = string.Empty;

    /// <summary>The tier levels for this Cheermote.</summary>
    [JsonPropertyName("tiers")]
    public CheermoteTierData[]? Tiers { get; init; }

    /// <summary>The type of Cheermote. Possible values: global_first_party, global_third_party, channel_custom, display_only, sponsored.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The display order in the Bits card.</summary>
    [JsonPropertyName("order")]
    public int Order { get; init; }

    /// <summary>The UTC date and time (in RFC3339 format) when the Cheermote was last updated.</summary>
    [JsonPropertyName("last_updated")]
    public string LastUpdated { get; init; } = string.Empty;

    /// <summary>Whether this Cheermote provides a charitable contribution match during campaigns.</summary>
    [JsonPropertyName("is_charitable")]
    public bool IsCharitable { get; init; }
}
