namespace TwitchSharp.Api.Authentication;

/// <summary>
/// Delegate invoked when a token is refreshed, allowing the consumer to persist it.
/// </summary>
/// <param name="tokenSet">The newly refreshed token set.</param>
/// <param name="cancellationToken">A token to cancel the persistence operation.</param>
/// <returns>A task representing the persistence operation.</returns>
public delegate Task TokenPersistenceCallbackAsync(TwitchTokenSet tokenSet, CancellationToken cancellationToken);

/// <summary>
/// Configuration options for token management behavior.
/// </summary>
public sealed class TokenManagerOptions
{
    /// <summary>
    /// An optional callback invoked when a token is refreshed. Use this to persist refreshed tokens.
    /// If the callback throws, the refreshed token will not be used and a
    /// <see cref="TwitchApiException"/> with code <see cref="TwitchErrorCodes.TokenPersistenceFailed"/> will be thrown.
    /// </summary>
    public TokenPersistenceCallbackAsync? OnTokenRefreshed { get; set; }
}
