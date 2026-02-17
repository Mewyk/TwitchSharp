using System.Text.Json.Serialization;

namespace TwitchSharp.EventSub.Internal;

/// <summary>
/// Source-generated JSON serializer context for EventSub WebSocket message types.
/// </summary>
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(EventSubWsMessage))]
internal partial class EventSubWsJsonContext : JsonSerializerContext;
