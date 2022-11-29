using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Deals;

public class HubspotDealClient<T> : HubspotClient<T>, IHubspotClient where T : class, IHubspotDeal
{
    private readonly HttpClient _httpClient;

    public HubspotDealClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(HubspotSettings.BaseUrl ?? throw new ArgumentException(nameof(HubspotSettings.BaseUrl)))
        };

        Init(this);
    }

    public HubspotDealClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        Init(this);
    }

    public HttpClient HttpClient()
    {
        return _httpClient;
    }

    public string EntityBaseUrl => "crm/v3";
}