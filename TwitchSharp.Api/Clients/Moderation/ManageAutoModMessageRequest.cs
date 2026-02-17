using System.Text.Json.Serialization;
using TwitchSharp.Generators;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Request body for the Manage Held AutoMod Messages endpoint.
/// </summary>
[GenerateWithMethods]
public sealed partial record ManageAutoModMessageRequest
{
    /// <summary>The moderator who is approving or denying the held message.</summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = string.Empty;

    /// <summary>The ID of the message to allow or deny.</summary>
    [JsonPropertyName("msg_id")]
    public string MessageId { get; init; } = string.Empty;

    /// <summary>The action to take (ALLOW or DENY).</summary>
    [JsonPropertyName("action")]
    public string Action { get; init; } = string.Empty;
}
