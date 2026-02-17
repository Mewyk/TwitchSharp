using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the active extensions data containing panel, overlay, and component slots.
/// </summary>
[GenerateWithMethods]
public sealed partial record ActiveExtensionsData
{
    /// <summary>The panel extension slots, keyed by slot number ("1", "2", etc.).</summary>
    [JsonPropertyName("panel")]
    public Dictionary<string, ActiveExtensionSlotData>? Panel { get; init; }

    /// <summary>The overlay extension slots, keyed by slot number ("1", "2", etc.).</summary>
    [JsonPropertyName("overlay")]
    public Dictionary<string, ActiveExtensionSlotData>? Overlay { get; init; }

    /// <summary>The component extension slots, keyed by slot number ("1", "2", etc.).</summary>
    [JsonPropertyName("component")]
    public Dictionary<string, ActiveExtensionSlotData>? Component { get; init; }
}
