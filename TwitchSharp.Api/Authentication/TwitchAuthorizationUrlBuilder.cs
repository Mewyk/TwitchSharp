namespace TwitchSharp.Api.Authentication;

/// <summary>
/// Fluent builder for constructing Twitch OAuth authorization URLs.
/// Used to generate browser redirect URLs for the Authorization Code and Implicit Grant flows.
/// </summary>
public sealed class TwitchAuthorizationUrlBuilder
{
    private static readonly Uri AuthorizeBaseUri = new("https://id.twitch.tv/oauth2/authorize");

    private string? _clientId;
    private string? _redirectUri;
    private string _responseType = "code";
    private readonly List<string> _scopes = [];
    private string? _state;
    private bool _forceVerify;
    private string? _codeChallenge;
    private string? _codeChallengeMethod;
    private string? _nonce;
    private string? _claims;

    private TwitchAuthorizationUrlBuilder()
    {
    }

    /// <summary>
    /// Creates a new authorization URL builder instance.
    /// </summary>
    public static TwitchAuthorizationUrlBuilder Create() => new();

    /// <summary>
    /// Sets the application's client ID. Required.
    /// </summary>
    /// <param name="clientId">The registered client ID.</param>
    /// <returns>This builder for chaining.</returns>
    public TwitchAuthorizationUrlBuilder WithClientId(string clientId)
    {
        ArgumentNullException.ThrowIfNull(clientId);
        _clientId = clientId;
        return this;
    }

    /// <summary>
    /// Sets the redirect URI where the authorization response is sent. Required.
    /// Must match one of the URIs registered with the application.
    /// </summary>
    /// <param name="redirectUri">The redirect URI.</param>
    /// <returns>This builder for chaining.</returns>
    public TwitchAuthorizationUrlBuilder WithRedirectUri(string redirectUri)
    {
        ArgumentNullException.ThrowIfNull(redirectUri);
        _redirectUri = redirectUri;
        return this;
    }

    /// <summary>
    /// Configures the builder for the Authorization Code Grant flow (response_type=code).
    /// This is the default behavior.
    /// </summary>
    /// <returns>This builder for chaining.</returns>
    public TwitchAuthorizationUrlBuilder ForAuthorizationCode()
    {
        _responseType = "code";
        return this;
    }

    /// <summary>
    /// Configures the builder for the Implicit Grant flow (response_type=token).
    /// The access token is returned directly in the URL fragment.
    /// </summary>
    /// <returns>This builder for chaining.</returns>
    public TwitchAuthorizationUrlBuilder ForImplicitGrant()
    {
        _responseType = "token";
        return this;
    }

    /// <summary>
    /// Adds one or more OAuth scopes to the authorization request.
    /// Use constants from <see cref="TwitchScopes"/> for type-safe scope values.
    /// </summary>
    /// <param name="scopes">The scopes to request.</param>
    /// <returns>This builder for chaining.</returns>
    public TwitchAuthorizationUrlBuilder WithScopes(params string[] scopes)
    {
        ArgumentNullException.ThrowIfNull(scopes);
        _scopes.AddRange(scopes);
        return this;
    }

    /// <summary>
    /// Sets the state parameter for CSRF protection. Strongly recommended.
    /// This value is returned unchanged in the authorization response.
    /// </summary>
    /// <param name="state">An opaque value used to maintain state between the request and callback.</param>
    /// <returns>This builder for chaining.</returns>
    public TwitchAuthorizationUrlBuilder WithState(string state)
    {
        ArgumentNullException.ThrowIfNull(state);
        _state = state;
        return this;
    }

    /// <summary>
    /// Forces the user to re-authorize the application, even if they have previously authorized it.
    /// </summary>
    /// <param name="force">Whether to force re-verification. Default is true.</param>
    /// <returns>This builder for chaining.</returns>
    public TwitchAuthorizationUrlBuilder ForceVerify(bool force = true)
    {
        _forceVerify = force;
        return this;
    }

    /// <summary>
    /// Adds a PKCE code challenge to the authorization request using the S256 method.
    /// Use <see cref="PkceChallenge.Generate()"/> to create the code verifier and challenge pair.
    /// </summary>
    /// <param name="codeChallenge">The S256 code challenge derived from the code verifier.</param>
    /// <returns>This builder for chaining.</returns>
    public TwitchAuthorizationUrlBuilder WithPkce(string codeChallenge)
    {
        ArgumentNullException.ThrowIfNull(codeChallenge);
        _codeChallenge = codeChallenge;
        _codeChallengeMethod = "S256";
        return this;
    }

    /// <summary>
    /// Sets the OIDC nonce parameter for anti-replay protection.
    /// The nonce is included in the ID token and should be validated by the client.
    /// </summary>
    /// <param name="nonce">A random, unique value tied to the user's session.</param>
    /// <returns>This builder for chaining.</returns>
    public TwitchAuthorizationUrlBuilder WithNonce(string nonce)
    {
        ArgumentNullException.ThrowIfNull(nonce);
        _nonce = nonce;
        return this;
    }

    /// <summary>
    /// Sets the OIDC claims parameter to request specific claims in the ID token.
    /// </summary>
    /// <param name="claimsJson">A JSON string specifying the requested claims per the OIDC claims parameter.</param>
    /// <returns>This builder for chaining.</returns>
    public TwitchAuthorizationUrlBuilder WithClaims(string claimsJson)
    {
        ArgumentNullException.ThrowIfNull(claimsJson);
        _claims = claimsJson;
        return this;
    }

    /// <summary>
    /// Configures the builder for an OIDC Authorization Code flow.
    /// Sets response_type to "code" and adds the "openid" scope.
    /// </summary>
    /// <returns>This builder for chaining.</returns>
    public TwitchAuthorizationUrlBuilder ForOidcAuthorizationCode()
    {
        _responseType = "code";
        if (!_scopes.Contains("openid"))
        {
            _scopes.Add("openid");
        }
        return this;
    }

    /// <summary>
    /// Builds the authorization URL from the configured parameters.
    /// </summary>
    /// <returns>The fully constructed authorization URL.</returns>
    /// <exception cref="InvalidOperationException">Thrown when required parameters (ClientId, RedirectUri) are not set.</exception>
    public Uri Build()
    {
        if (string.IsNullOrEmpty(_clientId))
        {
            throw new InvalidOperationException("ClientId is required. Call WithClientId() before Build().");
        }

        if (string.IsNullOrEmpty(_redirectUri))
        {
            throw new InvalidOperationException("RedirectUri is required. Call WithRedirectUri() before Build().");
        }

        var parameters = new List<string>(6)
        {
            $"client_id={Uri.EscapeDataString(_clientId)}",
            $"redirect_uri={Uri.EscapeDataString(_redirectUri)}",
            $"response_type={_responseType}"
        };

        if (_scopes.Count > 0)
        {
            var scopeValue = string.Join(' ', _scopes);
            parameters.Add($"scope={Uri.EscapeDataString(scopeValue)}");
        }

        if (_state is not null)
        {
            parameters.Add($"state={Uri.EscapeDataString(_state)}");
        }

        if (_forceVerify)
        {
            parameters.Add("force_verify=true");
        }

        if (_codeChallenge is not null)
        {
            parameters.Add($"code_challenge={Uri.EscapeDataString(_codeChallenge)}");
        }

        if (_codeChallengeMethod is not null)
        {
            parameters.Add($"code_challenge_method={_codeChallengeMethod}");
        }

        if (_nonce is not null)
        {
            parameters.Add($"nonce={Uri.EscapeDataString(_nonce)}");
        }

        if (_claims is not null)
        {
            parameters.Add($"claims={Uri.EscapeDataString(_claims)}");
        }

        var query = string.Join('&', parameters);
        return new Uri($"{AuthorizeBaseUri}?{query}");
    }
}
