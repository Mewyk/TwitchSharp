using System.Text.Json.Serialization;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Represents the top-level Hype Train status containing current state and records.
/// </summary>
public sealed record HypeTrainStatusData
{
    /// <summary>Describes the current Hype Train. Null if a Hype Train is not active.</summary>
    [JsonPropertyName("current")]
    public HypeTrainData? Current { get; init; }

    /// <summary>Information about the channel's all-time Hype Train record. Null if no Hype Train has occurred.</summary>
    [JsonPropertyName("all_time_high")]
    public HypeTrainRecordData? AllTimeHigh { get; init; }

    /// <summary>Information about the channel's shared Hype Train record. Null if no shared Hype Train has occurred.</summary>
    [JsonPropertyName("shared_all_time_high")]
    public HypeTrainRecordData? SharedAllTimeHigh { get; init; }
}
