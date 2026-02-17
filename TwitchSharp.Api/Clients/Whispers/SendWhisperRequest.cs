using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Send Whisper endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record SendWhisperRequest
{
    /// <summary>The whisper message to send. Must not be empty.</summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;
}
