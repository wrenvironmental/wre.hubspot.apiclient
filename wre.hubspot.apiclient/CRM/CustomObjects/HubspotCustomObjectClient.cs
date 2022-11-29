using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.CustomObjects;

public class HubspotCustomObjectClient<T> : HubspotClient<T>, IHubspotClient where T : class, IHubspotCustomEntity
{
    private readonly HttpClient _httpClient;

    public HubspotCustomObjectClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(HubspotSettings.BaseUrl ?? throw new ArgumentException(nameof(HubspotSettings.BaseUrl)))
        };

        Init(this);
    }

    public HubspotCustomObjectClient(HttpClient httpClient)
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