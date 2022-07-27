namespace wre.hubspot.apiclient.Common;

public class HubspotSecondlyManager : DelegatingHandler
{
    private ManualResetEventSlim threadController;
    private readonly System.Timers.Timer timer;

    public HubspotSecondlyManager(int internalInSeconds = 10)
    {
        threadController = new ManualResetEventSlim();
        timer = new System.Timers.Timer(internalInSeconds * 60);
        timer.Elapsed += Timer_Elapsed;
    }

    private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        threadController.Set();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        var remaining = 0;
        
        response.Headers.TryGetValues("X-HubSpot-RateLimit-Remaining", out var r);
        if (r != null && r.TryGetNonEnumeratedCount(out var qtd3) && qtd3 == 1)
        {
            remaining = Convert.ToInt32(r.First());
        }

        if (remaining <= 10)
        {
            threadController.Wait(cancellationToken);
        }

        return response;
    }
}