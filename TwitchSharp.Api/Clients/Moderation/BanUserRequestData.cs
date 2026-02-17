using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// The data object within a Ban User request.
/// </summary>
[GenerateWithMethods]
public sealed partial record BanUserRequestData
{
    /// <summary>The ID of the user to ban or put in a timeout. Required.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The timeout duration in seconds (1-1209600). Omit for permanent ban.</summary>
    [JsonPropertyName("duration")]
    public int? Duration { get; init; }

    /// <summary>The reason for the ban (max 500 characters).</summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; init; }
}
