using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Warn Chat User endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record WarnChatUserRequest
{
    /// <summary>The warning data containing the user ID and reason.</summary>
    [JsonPropertyName("data")]
    public WarnChatUserRequestData Data { get; init; } = new();
}
