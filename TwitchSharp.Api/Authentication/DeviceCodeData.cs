using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Authentication;

/// <summary>
/// Represents the response from the Twitch device code endpoint (POST /oauth2/device).
/// Used to initiate the device code authorization flow.
/// </summary>
public sealed record DeviceCodeData
{
    /// <summary>The device verification code used when polling for token completion.</summary>
    [JsonPropertyName("device_code")]
    public string DeviceCode { get; init; } = string.Empty;

    /// <summary>The number of seconds until the device code expires.</summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }

    /// <summary>The minimum number of seconds to wait between polling requests.</summary>
    [JsonPropertyName("interval")]
    public int Interval { get; init; }

    /// <summary>The code the user enters at the verification URI to authorize the device.</summary>
    [JsonPropertyName("user_code")]
    public string UserCode { get; init; } = string.Empty;

    /// <summary>The URL where the user navigates to enter their user code and authorize the device.</summary>
    [JsonPropertyName("verification_uri")]
    public string VerificationUri { get; init; } = string.Empty;
}
