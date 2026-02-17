using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Events;

/// <summary>Represents the body content of a whisper message.</summary>
public sealed record WhisperBodyData
{
    /// <summary>The text content of the whisper.</summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;
}
