using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private ContentClassificationClient? _contentClassification;

    /// <summary>
    /// Gets the Content Classification Labels API client.
    /// </summary>
    public ContentClassificationClient ContentClassification => _contentClassification ??= new ContentClassificationClient(_httpClient);
}
