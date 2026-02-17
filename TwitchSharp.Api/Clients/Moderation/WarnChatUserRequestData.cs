using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// The data object within a Warn Chat User request.
/// </summary>
[GenerateWithMethods]
public sealed partial record WarnChatUserRequestData
{
    /// <summary>The ID of the user to warn. Required.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The reason for the warning (max 500 characters). Required.</summary>
    [JsonPropertyName("reason")]
    public string Reason { get; init; } = string.Empty;
}
