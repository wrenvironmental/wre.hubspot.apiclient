using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Products;

public class HubspotProductClient<T> : HubspotClient<T>, IHubspotClient where T : class, IHubspotProduct
{
    private readonly HttpClient _httpClient;

    public HubspotProductClient(string baseUrl)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };

        Init(this);
    }

    public HttpClient HttpClient()
    {
        return _httpClient;
    }

    public string EntityBaseUrl => "crm/v3";
}