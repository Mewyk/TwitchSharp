using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Internal request body wrapper for Update User Extensions.
/// </summary>
internal sealed record ActiveExtensionsPayload
{
    [JsonPropertyName("data")]
    public ActiveExtensionsData? Data { get; init; }
}
