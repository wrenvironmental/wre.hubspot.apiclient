using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Products;

public class HubspotProductClient<T> : HubspotClient<T>, IHubspotClient where T : class, IHubspotProduct
{
    private readonly HttpClient _httpClient;

    public HubspotProductClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(HubspotSettings.BaseUrl ?? throw new ArgumentException(nameof(HubspotSettings.BaseUrl)))
        };

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {HubspotSettings.AccessToken}");
        Init(this);
    }

    public HubspotProductClient(HttpClient httpClient)
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