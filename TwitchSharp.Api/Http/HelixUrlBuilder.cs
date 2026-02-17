using System.Text;

namespace TwitchSharp.Api.Http;

/// <summary>
/// Internal utility for building Helix API URL paths with query string parameters.
/// </summary>
internal struct HelixUrlBuilder
{
    private readonly string _path;
    private StringBuilder? _query;

    public HelixUrlBuilder(string path)
    {
        _path = path;
        _query = null;
    }

    public void Add(string key, string? value)
    {
        if (value is null) return;
        AppendSeparator().Append(key).Append('=').Append(Uri.EscapeDataString(value));
    }

    public void Add(string key, int? value)
    {
        if (value is null) return;
        AppendSeparator().Append(key).Append('=').Append(value.Value);
    }

    public void Add(string key, bool? value)
    {
        if (value is null) return;
        AppendSeparator().Append(key).Append('=').Append(value.Value ? "true" : "false");
    }

    public void Add(string key, double? value)
    {
        if (value is null) return;
        AppendSeparator().Append(key).Append('=').Append(value.Value);
    }

    public void AddRepeated(string key, IEnumerable<string>? values)
    {
        if (values is null) return;
        foreach (var value in values)
        {
            AppendSeparator().Append(key).Append('=').Append(Uri.EscapeDataString(value));
        }
    }

    public readonly string Build()
    {
        return _query is null ? _path : string.Concat(_path, _query.ToString());
    }

    private StringBuilder AppendSeparator()
    {
        var stringBuilder = _query;
        if (stringBuilder is null)
        {
            stringBuilder = new StringBuilder();
            stringBuilder.Append('?');
            _query = stringBuilder;
        }
        else
        {
            stringBuilder.Append('&');
        }

        return stringBuilder;
    }
}
