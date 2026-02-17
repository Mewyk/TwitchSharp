using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>
/// Represents an extension Bits transaction create event.
/// This event type is webhook-only and cannot be used with WebSocket.
/// </summary>
public sealed record ExtensionBitsTransactionCreateEvent
{
    /// <summary>The client identifier of the extension.</summary>
    [JsonPropertyName("extension_client_id")]
    public string ExtensionClientId { get; init; } = string.Empty;

    /// <summary>The transaction identifier.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>The broadcaster's user identifier.</summary>
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; init; } = string.Empty;

    /// <summary>The broadcaster's login name.</summary>
    [JsonPropertyName("broadcaster_user_login")]
    public string BroadcasterUserLogin { get; init; } = string.Empty;

    /// <summary>The broadcaster's display name.</summary>
    [JsonPropertyName("broadcaster_user_name")]
    public string BroadcasterUserName { get; init; } = string.Empty;

    /// <summary>The user identifier of the user who made the transaction.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The login name of the user who made the transaction.</summary>
    [JsonPropertyName("user_login")]
    public string UserLogin { get; init; } = string.Empty;

    /// <summary>The display name of the user who made the transaction.</summary>
    [JsonPropertyName("user_name")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>The product information for this transaction.</summary>
    [JsonPropertyName("product")]
    public ExtensionBitsProductData Product { get; init; } = new();
}
