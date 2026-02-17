using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents a user.update v1 event, fired when a user updates their account.</summary>
public sealed record UserUpdateEvent
{
    /// <summary>The user ID of the updated user.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The user login of the updated user.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The user display name of the updated user.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The user's email address, or null if the scope was not included.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    /// <summary>Whether the user's email has been verified, or null if the scope was not included.</summary>
    [JsonPropertyName("email_verified")]
    public bool? EmailVerified { get; init; }

    /// <summary>The user's profile description.</summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;
}
