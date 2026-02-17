using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Add Blocked Term endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record AddBlockedTermRequest
{
    /// <summary>The word or phrase to block (2-500 characters).</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;
}
