using Microsoft.Extensions.Logging;
using Timer = System.Timers.Timer;

namespace wre.hubspot.apiclient.Common;

public class HubspotSecondlyManager : DelegatingHandler
{
    private readonly SemaphoreSlim _perSecondThreadManager;
    private readonly SemaphoreSlim _perTenSecondsThreadManager;
    private readonly Timer _perSecondTimer = new Timer(1000);
    private readonly Timer _perTenSecondsTimer = new Timer(10000);
    private readonly ILogger<HubspotSecondlyManager>? _logger;
    private readonly object _lock = new object();

    public int MaxCallsPerSecond { get; }
    public int MaxCallsEveryTenSeconds { get; }

    public HubspotSecondlyManager(ILogger<HubspotSecondlyManager> logger, int maxCallsPerSecond, int maxCallsEveryTenSeconds)
        : this(maxCallsPerSecond, maxCallsEveryTenSeconds)
    {
        _logger = logger;
    }

    public HubspotSecondlyManager(int maxCallsPerSecond, int maxCallsEveryTenSeconds)
    {
        _perSecondThreadManager = new SemaphoreSlim(maxCallsPerSecond, maxCallsPerSecond);
        _perTenSecondsThreadManager = new SemaphoreSlim(maxCallsEveryTenSeconds, maxCallsEveryTenSeconds);

        _perSecondTimer.Elapsed += OneSecond_Elapsed;
        _perSecondTimer.Start();

        _perTenSecondsTimer.Elapsed += TenSeconds_Elapsed;
        _perTenSecondsTimer.Start();
        MaxCallsPerSecond = maxCallsPerSecond;
        MaxCallsEveryTenSeconds = maxCallsEveryTenSeconds;
    }

    private void OneSecond_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        lock (_lock)
        {
            if (_perSecondThreadManager.CurrentCount == 0)
            {
                try
                {
                    _perSecondThreadManager.Release(MaxCallsPerSecond);
                }
                catch (SemaphoreFullException) { }

                _logger?.LogInformation($"[Second] - Releasing {MaxCallsPerSecond} positions");
            }
        }
    }

    private void TenSeconds_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        lock (_lock)
        {
            if (_perTenSecondsThreadManager.CurrentCount == 0)
            {
                try
                {
                    _perTenSecondsThreadManager.Release(MaxCallsEveryTenSeconds);
                }
                catch (SemaphoreFullException) { }

                _logger?.LogInformation($"[10 Seconds] - Releasing {MaxCallsEveryTenSeconds} positions");
            }
        }        
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //Entering both thread managers
        _perSecondThreadManager.Wait(cancellationToken);
        _perTenSecondsThreadManager.Wait(cancellationToken);

        var response = await base.SendAsync(request, cancellationToken);
        return response;
    }
}