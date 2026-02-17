using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents a container of stream markers for a user, returned by the Get Stream Markers endpoint.
/// </summary>
public sealed record StreamMarkerContainerData
{
    /// <summary>The user's ID.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user's display name.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The user's login name.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The videos containing markers.</summary>
    [JsonPropertyName("videos")]
    public StreamMarkerVideoData[] Videos { get; init; } = [];
}
