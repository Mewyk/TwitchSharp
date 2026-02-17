using TwitchSharp.Api.Authentication;
using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private AuthenticationClient? _authentication;

    /// <summary>
    /// Gets the Authentication client for token lifecycle operations including validation, revocation,
    /// authorization code exchange, and device code flow.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the client was created without OAuth client support.
    /// </exception>
    public AuthenticationClient Authentication => _authentication ??= CreateAuthenticationClient();

    /// <summary>
    /// Exchanges an authorization code for tokens and automatically sets the user token
    /// on this client for subsequent user-authenticated requests.
    /// </summary>
    /// <param name="code">The authorization code received from the redirect.</param>
    /// <param name="redirectUri">The redirect URI that was used in the authorization request.</param>
    /// <param name="codeVerifier">Optional PKCE code verifier. Required if a code challenge was used in the authorization request.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The acquired token set.</returns>
    public async Task<TwitchTokenSet> ExchangeCodeAndSetUserTokenAsync(
        string code,
        string redirectUri,
        string? codeVerifier = null,
        CancellationToken cancellationToken = default)
    {
        var tokenSet = await Authentication.ExchangeCodeAsync(code, redirectUri, codeVerifier, cancellationToken).ConfigureAwait(false);
        SetUserToken(tokenSet);
        return tokenSet;
    }

    /// <summary>
    /// Completes a device code authorization flow by polling until the user authorizes or the code expires.
    /// On success, automatically sets the user token on this client.
    /// </summary>
    /// <param name="deviceCode">The device code data from <see cref="AuthenticationClient.StartDeviceFlowAsync"/>.</param>
    /// <param name="scopes">The same scopes used when starting the device flow.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The acquired token set.</returns>
    public async Task<TwitchTokenSet> CompleteDeviceFlowAsync(
        DeviceCodeData deviceCode,
        string[] scopes,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(deviceCode);

        var pollInterval = TimeSpan.FromSeconds(Math.Max(deviceCode.Interval, 5));
        var deadline = DateTimeOffset.UtcNow.AddSeconds(deviceCode.ExpiresIn);

        while (DateTimeOffset.UtcNow < deadline)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Task.Delay(pollInterval, cancellationToken).ConfigureAwait(false);

            try
            {
                var tokenSet = await Authentication.PollDeviceFlowAsync(deviceCode.DeviceCode, scopes, cancellationToken)
                    .ConfigureAwait(false);
                SetUserToken(tokenSet);
                return tokenSet;
            }
            catch (TwitchApiException exception) when (exception.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                // authorization_pending or slow_down — continue polling
            }
        }

        throw new TwitchApiException(
            TwitchErrorCodes.Timeout,
            "Device code authorization timed out. The user did not authorize within the allowed time.",
            endpoint: "oauth2/token");
    }

    private AuthenticationClient CreateAuthenticationClient()
    {
        if (_oauthClientFactory is null)
        {
            throw new InvalidOperationException(
                "OAuth client factory is not available. Authentication operations require an OAuth HttpClient.");
        }

        return new AuthenticationClient(_options, _oauthClientFactory);
    }
}
