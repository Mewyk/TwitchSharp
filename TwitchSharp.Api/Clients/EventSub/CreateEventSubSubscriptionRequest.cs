using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Create EventSub Subscription endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record CreateEventSubSubscriptionRequest
{
    /// <summary>The subscription type. Required.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>The subscription version. Required.</summary>
    [JsonPropertyName("version")]
    public string Version { get; init; } = string.Empty;

    /// <summary>The subscription condition as key-value pairs. Required.</summary>
    [JsonPropertyName("condition")]
    public Dictionary<string, string> Condition { get; init; } = new();

    /// <summary>The transport configuration. Required.</summary>
    [JsonPropertyName("transport")]
    public CreateEventSubTransportRequest Transport { get; init; } = new();
}
