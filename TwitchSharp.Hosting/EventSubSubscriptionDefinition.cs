namespace TwitchSharp.Hosting;

/// <summary>
/// Defines an EventSub subscription to be automatically created when the hosted service connects.
/// </summary>
/// <param name="Type">The subscription type (e.g., "channel.follow").</param>
/// <param name="Version">The subscription version (e.g., "2").</param>
/// <param name="Condition">The subscription condition as key-value pairs.</param>
public sealed record EventSubSubscriptionDefinition(
    string Type,
    string Version,
    Dictionary<string, string> Condition);
