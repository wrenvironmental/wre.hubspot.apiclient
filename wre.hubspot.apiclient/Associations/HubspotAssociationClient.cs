using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Associations;

public class HubspotAssociationClient : IHubspotClient
{
    private readonly HttpClient _httpClient;

    public HubspotAssociationClient(string baseUrl)
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

    public string EntityBaseUrl => "crm/v3/associations";
}