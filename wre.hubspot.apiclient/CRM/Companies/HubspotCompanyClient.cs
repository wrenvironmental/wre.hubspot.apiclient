using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Companies
{
    public class HubspotCompanyClient<T> : HubspotClient<T>, IHubspotClient where T : class, IHubspotCompany
    {
        private readonly HttpClient _httpClient;

        public HubspotCompanyClient(string baseUrl, string apiKey)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            Init(this);
        }

        public HubspotCompanyClient(HttpClient httpClient)
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
}
