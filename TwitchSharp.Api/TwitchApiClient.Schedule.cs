using TwitchSharp.Api.Clients;

namespace TwitchSharp.Api;

public sealed partial class TwitchApiClient
{
    private ScheduleClient? _schedule;

    /// <summary>
    /// Gets the Schedule API client for managing broadcast schedules.
    /// </summary>
    public ScheduleClient Schedule => _schedule ??= new ScheduleClient(_httpClient);
}
