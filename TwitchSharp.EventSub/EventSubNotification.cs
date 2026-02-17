using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using TwitchSharp.Api.Clients;

namespace TwitchSharp.EventSub;

/// <summary>
/// An EventSub notification containing event data for a subscription.
/// </summary>
public sealed record EventSubNotification : EventSubMessage
{
    /// <summary>The subscription type that triggered this notification.</summary>
    public string SubscriptionType { get; init; } = string.Empty;

    /// <summary>The subscription version.</summary>
    public string SubscriptionVersion { get; init; } = string.Empty;

    /// <summary>The subscription that generated this notification.</summary>
    public EventSubSubscriptionData Subscription { get; init; } = new();

    /// <summary>The raw event data as a JSON element. Use <see cref="DeserializeEvent{T}"/> for typed access.</summary>
    public JsonElement Event { get; init; }

    /// <summary>
    /// Deserializes the event data into a strongly-typed object using a source-generated JSON type info.
    /// </summary>
    /// <typeparam name="T">The event data type.</typeparam>
    /// <param name="jsonTypeInfo">The source-generated JSON type info for AOT-safe deserialization.</param>
    /// <returns>The deserialized event data.</returns>
    public T DeserializeEvent<T>(JsonTypeInfo<T> jsonTypeInfo)
    {
        return Event.Deserialize(jsonTypeInfo)
            ?? throw new InvalidOperationException($"Failed to deserialize event data as {typeof(T).Name}.");
    }
}
