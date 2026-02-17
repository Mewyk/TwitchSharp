using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Internal response wrapper for Get/Update User Active Extensions.
/// The "data" field is an object (not an array), so this uses a custom shape.
/// </summary>
internal sealed record ActiveExtensionsResponse
{
    [JsonPropertyName("data")]
    public ActiveExtensionsData? Data { get; init; }
}
