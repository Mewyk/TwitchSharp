using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a user.authorization.revoke v1 event, fired when a user revokes authorization from a client.</summary>
public sealed record UserAuthorizationRevokeEvent
{
    /// <summary>The client ID of the application that had authorization revoked.</summary>
    [JsonPropertyName("client_id")]
    public string ClientId { get; init; } = string.Empty;

    /// <summary>The user ID of the user who revoked authorization.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login of the user who revoked authorization, or null if unavailable.</summary>
    [JsonPropertyName("user_login")]
    public string? UserLogin { get; init; }

    /// <summary>The user display name of the user who revoked authorization, or null if unavailable.</summary>
    [JsonPropertyName("user_name")]
    public string? UserName { get; init; }
}
