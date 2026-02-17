using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Ban User endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record BanUserRequest
{
    /// <summary>The ban data containing the user ID, duration, and reason.</summary>
    [JsonPropertyName("data")]
    public BanUserRequestData Data { get; init; } = new();
}
