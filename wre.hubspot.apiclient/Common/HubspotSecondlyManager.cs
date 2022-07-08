namespace wre.hubspot.apiclient.Common;

public class HubspotSecondlyManager : DelegatingHandler
{
    private ManualResetEventSlim threadController;
    private int timeWaited = 0;
    private object _lock = new ();

    public HubspotSecondlyManager()
    {
        threadController = new ManualResetEventSlim();
    }

    private void Wait()
    {
        lock (_lock)
        {
            Thread.Sleep(1000);
        }
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
            Wait();
            threadController.Wait(cancellationToken);
        }
        else
        {
            threadController.Set();
        }

        return response;
    }
}