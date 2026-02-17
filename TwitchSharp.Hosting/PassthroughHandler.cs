namespace TwitchSharp.Hosting;

/// <summary>
/// A no-op <see cref="DelegatingHandler"/> used as a placeholder when resilience is disabled.
/// </summary>
internal sealed class PassthroughHandler : DelegatingHandler
{
    /// <inheritdoc />
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken) =>
        base.SendAsync(request, cancellationToken);
}
