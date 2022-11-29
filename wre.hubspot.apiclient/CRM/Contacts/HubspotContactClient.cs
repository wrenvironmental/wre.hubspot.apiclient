using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Contacts;

public class HubspotContactClient<T> : HubspotClient<T>, IHubspotClient where T : class, IHubspotContact
{
    private readonly HttpClient _httpClient;    

    public HubspotContactClient(string baseUrl)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {HubspotSettings.AccessToken}");
        Init(this);
    }

    public HubspotContactClient(HttpClient httpClient)
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