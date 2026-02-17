using System.Text.Json;
using TwitchSharp.Api.Authentication;
using TwitchSharp.Extensions.Authentication.Json;

namespace TwitchSharp.Extensions.Authentication;

/// <summary>
/// Provides JSON file-based persistence for <see cref="TwitchTokenSet"/>.
/// Tokens are serialized using AOT-compatible source-generated JSON serialization.
/// </summary>
/// <remarks>
/// This store can be used standalone to save and load tokens, or wired into
/// <see cref="TokenManagerOptions.OnTokenRefreshed"/> via <see cref="CreatePersistenceCallback"/>
/// to automatically persist tokens when they are refreshed by the token manager.
/// </remarks>
public sealed class FileTokenStore
{
    private readonly string _filePath;

    /// <summary>
    /// Creates a new <see cref="FileTokenStore"/> that persists tokens to the specified file path.
    /// </summary>
    /// <param name="filePath">The file path where tokens will be stored as JSON.</param>
    public FileTokenStore(string filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);
        _filePath = filePath;
    }

    /// <summary>
    /// Saves a token set to the file, overwriting any existing content.
    /// </summary>
    /// <param name="tokenSet">The token set to persist.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task SaveAsync(TwitchTokenSet tokenSet, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(tokenSet);

        var storedData = new StoredTokenData
        {
            AccessToken = tokenSet.AccessToken,
            RefreshToken = tokenSet.RefreshToken,
            ExpiresAtUtc = tokenSet.ExpiresAtUtc,
            TokenType = tokenSet.TokenType,
            Scopes = [.. tokenSet.Scopes],
            IdToken = tokenSet.IdToken
        };

        await using var stream = new FileStream(
            _filePath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 4096,
            useAsync: true);

        await JsonSerializer.SerializeAsync(
            stream,
            storedData,
            AuthenticationExtensionsJsonContext.Default.StoredTokenData,
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Loads a token set from the file.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The loaded token set, or <c>null</c> if the file does not exist.</returns>
    public async Task<TwitchTokenSet?> LoadAsync(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(_filePath))
        {
            return null;
        }

        await using var stream = new FileStream(
            _filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096,
            useAsync: true);

        var storedData = await JsonSerializer.DeserializeAsync(
            stream,
            AuthenticationExtensionsJsonContext.Default.StoredTokenData,
            cancellationToken).ConfigureAwait(false);

        if (storedData is null)
        {
            return null;
        }

        return new TwitchTokenSet
        {
            AccessToken = storedData.AccessToken,
            RefreshToken = storedData.RefreshToken,
            ExpiresAtUtc = storedData.ExpiresAtUtc,
            TokenType = storedData.TokenType,
            Scopes = storedData.Scopes,
            IdToken = storedData.IdToken
        };
    }

    /// <summary>
    /// Deletes the stored token file.
    /// </summary>
    /// <returns><c>true</c> if the file existed and was deleted; <c>false</c> if it did not exist.</returns>
    public bool Delete()
    {
        if (!File.Exists(_filePath))
        {
            return false;
        }

        File.Delete(_filePath);
        return true;
    }

    /// <summary>
    /// Creates a <see cref="TokenPersistenceCallbackAsync"/> delegate suitable for use with
    /// <see cref="TokenManagerOptions.OnTokenRefreshed"/>. When the token manager refreshes
    /// a token, the new token set will be automatically saved to the file.
    /// </summary>
    /// <returns>A delegate that persists the refreshed token set to the file.</returns>
    public TokenPersistenceCallbackAsync CreatePersistenceCallback()
    {
        return (tokenSet, cancellationToken) => SaveAsync(tokenSet, cancellationToken);
    }
}
