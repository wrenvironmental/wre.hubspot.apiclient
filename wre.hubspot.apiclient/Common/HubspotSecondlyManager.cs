using Timer = System.Timers.Timer;

namespace wre.hubspot.apiclient.Common;

public class HubspotSecondlyManager : DelegatingHandler
{
    private readonly ManualResetEventSlim _threadController;

    public HubspotSecondlyManager(int internalInSeconds = 10)
    {
        _threadController = new ManualResetEventSlim();
        var timer = new Timer(internalInSeconds * 1000);
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }

    private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        _threadController.Set();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        if (!response.Headers.Contains("X-HubSpot-RateLimit-Remaining")) return response;

        var rateLimit = response.Headers.GetValues("X-HubSpot-RateLimit-Remaining").FirstOrDefault();
        if (rateLimit == null) return response;
        var remaining = Convert.ToInt32(rateLimit);
        if (remaining > 10) return response;
        Console.WriteLine("------------------ Awaiting for Secondly Rate Limit reset ------------------ ");
        if (_threadController.IsSet) _threadController.Reset();
        _threadController.Wait(cancellationToken);

        return response;
    }
}