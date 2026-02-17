using System.Net.Http.Json;
using TwitchSharp.Api.Http;
using TwitchSharp.Api.Json;

namespace TwitchSharp.Api.Clients;

/// <summary>
/// Client for Twitch Helix Schedule API endpoints.
/// </summary>
/// <remarks>All methods may throw <see cref="TwitchApiException"/> on API errors.</remarks>
public sealed class ScheduleClient
{
    private readonly HelixHttpClient _httpClient;

    internal ScheduleClient(HelixHttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets the broadcaster's streaming schedule.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="ids">The IDs of the schedule segments to get.</param>
    /// <param name="startTime">The UTC date and time to start returning schedule segments from.</param>
    /// <param name="first">The maximum number of items to return per page.</param>
    /// <param name="after">The cursor used to get the next page of results.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A page of schedule segments.</returns>
    public async Task<TwitchPage<ScheduleSegmentData>> GetChannelStreamScheduleAsync(
        string broadcasterId,
        IEnumerable<string>? ids = null,
        string? startTime = null,
        int? first = null,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("schedule");
        url.Add("broadcaster_id", broadcasterId);
        url.AddRepeated("id", ids);
        url.Add("start_time", startTime);
        url.Add("first", first);
        url.Add("after", after);

        var response = await _httpClient.SendAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.AppToken,
            TwitchApiJsonContext.Default.ScheduleResponse,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return new TwitchPage<ScheduleSegmentData>(
            response.Data?.Segments ?? [],
            response.Pagination?.Cursor);
    }

    /// <summary>
    /// Gets the broadcaster's streaming schedule as iCalendar data (RFC 5545).
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The iCalendar data as a string.</returns>
    public async Task<string> GetChannelICalendarAsync(
        string broadcasterId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("schedule/icalendar");
        url.Add("broadcaster_id", broadcasterId);

        return await _httpClient.SendRawAsync(
            HttpMethod.Get,
            url.Build(),
            TwitchAuthenticationMode.None,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the broadcaster's schedule settings (vacation mode).
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="isVacationEnabled">Whether vacation mode is enabled.</param>
    /// <param name="vacationStartTime">The UTC date and time of the vacation start.</param>
    /// <param name="vacationEndTime">The UTC date and time of the vacation end.</param>
    /// <param name="timezone">The timezone for the vacation schedule.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task UpdateChannelStreamScheduleAsync(
        string broadcasterId,
        bool? isVacationEnabled = null,
        string? vacationStartTime = null,
        string? vacationEndTime = null,
        string? timezone = null,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("schedule/settings");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("is_vacation_enabled", isVacationEnabled);
        url.Add("vacation_start_time", vacationStartTime);
        url.Add("vacation_end_time", vacationEndTime);
        url.Add("timezone", timezone);

        await _httpClient.SendAsync(
            HttpMethod.Patch,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new broadcast segment in the broadcaster's schedule.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="request">The create schedule segment parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The schedule data, or <see langword="null"/> if not found.</returns>
    public async Task<ScheduleData?> CreateChannelStreamScheduleSegmentAsync(
        string broadcasterId,
        CreateScheduleSegmentRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("schedule/segment");
        url.Add("broadcaster_id", broadcasterId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.CreateScheduleSegmentRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Post,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.ScheduleResponse,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data;
    }

    /// <summary>
    /// Updates an existing broadcast segment in the broadcaster's schedule.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="segmentId">The ID of the schedule segment to update.</param>
    /// <param name="request">The update schedule segment parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The schedule data, or <see langword="null"/> if not found.</returns>
    public async Task<ScheduleData?> UpdateChannelStreamScheduleSegmentAsync(
        string broadcasterId,
        string segmentId,
        UpdateScheduleSegmentRequest request,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("schedule/segment");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("id", segmentId);

        var content = JsonContent.Create(request, TwitchApiJsonContext.Default.UpdateScheduleSegmentRequest);

        var response = await _httpClient.SendAsync(
            HttpMethod.Patch,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            TwitchApiJsonContext.Default.ScheduleResponse,
            content,
            cancellationToken).ConfigureAwait(false);

        return response.Data;
    }

    /// <summary>
    /// Deletes a broadcast segment from the broadcaster's schedule.
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster.</param>
    /// <param name="segmentId">The ID of the schedule segment to delete.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public async Task DeleteChannelStreamScheduleSegmentAsync(
        string broadcasterId,
        string segmentId,
        CancellationToken cancellationToken = default)
    {
        var url = new HelixUrlBuilder("schedule/segment");
        url.Add("broadcaster_id", broadcasterId);
        url.Add("id", segmentId);

        await _httpClient.SendAsync(
            HttpMethod.Delete,
            url.Build(),
            TwitchAuthenticationMode.UserToken,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
