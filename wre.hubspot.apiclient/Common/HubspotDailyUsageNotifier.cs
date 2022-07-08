namespace wre.hubspot.apiclient.Common;

public class HubspotDailyUsageNotifier : DelegatingHandler
{
    private readonly float _threshold;
    public event EventHandler<HubspotDailyUsageStats>? OnDailyThresholdReached;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="threshold">Defines the percentage when the events starts to be fired</param>
    public HubspotDailyUsageNotifier(float threshold)
    {
        _threshold = threshold;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        var stats = new HubspotDailyUsageStats();
        response.Headers.TryGetValues("X-HubSpot-RateLimit-Daily", out var values);
        if (values != null && values.TryGetNonEnumeratedCount(out var qtd) && qtd == 1)
        {
            stats.AllowedRequestsPerDay = Convert.ToInt64(values.First());
        }
        response.Headers.TryGetValues("X-HubSpot-RateLimit-Daily-Remaining", out var values1);
        if (values1 != null && values1.TryGetNonEnumeratedCount(out var qtd2) && qtd2 == 1)
        {
            stats.RequestsPerDayRemaining = Convert.ToInt64(values1.First());
        }

        if (stats.AllowedRequestsPerDay <= 0) return response;

        var currentPercentage = 100 - (stats.RequestsPerDayRemaining * 100 / stats.AllowedRequestsPerDay);
        if (currentPercentage >= _threshold)
            OnDailyThresholdReached?.Invoke(this, stats);

        return response;
    }
}

public struct HubspotDailyUsageStats
{
    public long AllowedRequestsPerDay;
    public long RequestsPerDayRemaining;
}