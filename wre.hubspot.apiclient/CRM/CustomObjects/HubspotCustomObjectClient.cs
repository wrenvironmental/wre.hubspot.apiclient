using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.CustomObjects;

public class HubspotCustomObjectClient<T> : HubspotClient<T>, IHubspotClient where T : class, IHubspotCustomEntity
{
    private readonly HttpClient _httpClient;

    public HubspotCustomObjectClient(string baseUrl, string apiKey)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
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