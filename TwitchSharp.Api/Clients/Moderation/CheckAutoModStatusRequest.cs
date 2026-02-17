using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Check AutoMod Status endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record CheckAutoModStatusRequest
{
    /// <summary>The list of messages to check (1-100).</summary>
    [JsonPropertyName("data")]
    public AutoModMessageRequest[] Data { get; init; } = [];
}
