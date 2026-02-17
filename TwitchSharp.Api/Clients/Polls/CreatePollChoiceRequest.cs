using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// A poll choice in a Create Poll request.
/// </summary>
public sealed record CreatePollChoiceRequest
{
    /// <summary>The choice text (max 25 characters). Required.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;
}
