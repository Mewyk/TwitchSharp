using System.Net;
using System.Text;
using System.Web;

namespace TwitchSharp.Extensions.Authentication;

/// <summary>
/// A lightweight local HTTP listener that receives an OAuth authorization callback.
/// Starts an <see cref="HttpListener"/> on a localhost redirect URI, waits for the OAuth provider
/// to redirect the user's browser, extracts the authorization <c>code</c> and <c>state</c>
/// query parameters, serves a response page, and shuts down.
/// </summary>
/// <remarks>
/// This is intended for desktop, console, and CLI applications that need to complete
/// an OAuth Authorization Code flow without a web server.
/// </remarks>
public sealed class OAuthCallbackListener : IDisposable
{
    private const string DefaultSuccessHtml =
        """
        <!DOCTYPE html>
        <html><head><title>Authorization Complete</title></head>
        <body><h1>Authorization Complete</h1><p>You may close this window and return to the application.</p></body>
        </html>
        """;

    private const string DefaultErrorHtml =
        """
        <!DOCTYPE html>
        <html><head><title>Authorization Error</title></head>
        <body><h1>Authorization Error</h1><p>The authorization callback did not include an authorization code. Please try again.</p></body>
        </html>
        """;

    private readonly HttpListener _listener;
    private readonly string _redirectUri;
    private readonly OAuthCallbackListenerOptions _options;
    private bool _disposed;

    /// <summary>
    /// Creates a new <see cref="OAuthCallbackListener"/> with default options.
    /// </summary>
    /// <param name="redirectUri">
    /// The full localhost redirect URI to listen on (e.g., <c>http://localhost:3000/callback/</c>).
    /// Must end with a trailing slash for <see cref="HttpListener"/> compatibility.
    /// </param>
    public OAuthCallbackListener(string redirectUri)
        : this(redirectUri, options: null)
    {
    }

    /// <summary>
    /// Creates a new <see cref="OAuthCallbackListener"/> with custom options.
    /// </summary>
    /// <param name="redirectUri">
    /// The full localhost redirect URI to listen on (e.g., <c>http://localhost:3000/callback/</c>).
    /// Must end with a trailing slash for <see cref="HttpListener"/> compatibility.
    /// </param>
    /// <param name="options">Optional configuration for customizing the response HTML pages.</param>
    public OAuthCallbackListener(string redirectUri, OAuthCallbackListenerOptions? options)
    {
        ArgumentNullException.ThrowIfNull(redirectUri);

        // HttpListener requires a trailing slash on prefixes
        _redirectUri = redirectUri.EndsWith('/') ? redirectUri : redirectUri + "/";
        _options = options ?? new OAuthCallbackListenerOptions();
        _listener = new HttpListener();
        _listener.Prefixes.Add(_redirectUri);
    }

    /// <summary>
    /// Starts listening for the OAuth callback and blocks until the callback is received or the operation is cancelled.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to abort listening.</param>
    /// <returns>The <see cref="OAuthCallbackResult"/> containing the authorization code and optional state parameter.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the callback does not contain a <c>code</c> query parameter.</exception>
    /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled via <paramref name="cancellationToken"/>.</exception>
    public async Task<OAuthCallbackResult> ListenForCallbackAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        _listener.Start();

        try
        {
            var contextTask = _listener.GetContextAsync();

            // Register cancellation to abort the listener
            using var registration = cancellationToken.Register(() => _listener.Stop());

            HttpListenerContext context;
            try
            {
                context = await contextTask.ConfigureAwait(false);
            }
            catch (HttpListenerException) when (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException(cancellationToken);
            }

            var queryString = context.Request.QueryString;
            var code = queryString["code"];
            var state = queryString["state"];

            if (string.IsNullOrEmpty(code))
            {
                await SendResponseAsync(context, _options.ErrorHtml ?? DefaultErrorHtml, 400).ConfigureAwait(false);
                throw new InvalidOperationException(
                    "The OAuth callback did not contain a 'code' query parameter. " +
                    "The user may have denied the authorization request.");
            }

            await SendResponseAsync(context, _options.SuccessHtml ?? DefaultSuccessHtml, 200).ConfigureAwait(false);

            return new OAuthCallbackResult
            {
                Code = code,
                State = state
            };
        }
        finally
        {
            _listener.Stop();
        }
    }

    private static async Task SendResponseAsync(HttpListenerContext context, string html, int statusCode)
    {
        var response = context.Response;
        response.StatusCode = statusCode;
        response.ContentType = "text/html; charset=utf-8";

        var buffer = Encoding.UTF8.GetBytes(html);
        response.ContentLength64 = buffer.Length;

        await response.OutputStream.WriteAsync(buffer).ConfigureAwait(false);
        await response.OutputStream.FlushAsync().ConfigureAwait(false);
        response.Close();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        ((IDisposable)_listener).Dispose();
    }
}
