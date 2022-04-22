using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Deals;

public class DealClient : IHubspotClient
{
    private readonly HttpClient _httpClient;

    public DealClient(string baseUrl)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
    }

    public HttpClient HttpClient()
    {
        return _httpClient;
    }

    public string EntityBaseUrl => "crm/v3";
}