using wre.hubspot.apiclient.Associations;
using wre.hubspot.apiclient.CRM;

namespace wre.hubspot.apiclient
{
    public class HubspotClient
    {
        public HubspotClient()
        {
            var baseUrl = HubspotSettings.BaseUrl ?? throw new ArgumentException(nameof(HubspotSettings.BaseUrl));
            var apiKey = HubspotSettings.AccessToken ?? throw new ArgumentException(nameof(HubspotSettings.AccessToken));
            CRM = new CRMObjects(baseUrl, apiKey);
            Associations = new HubspotAssociationClient<HubspotAssociationEntity>(baseUrl, apiKey);
        }

        public HubspotClient(HttpClient httpClient)
        {
            CRM = new CRMObjects(httpClient);
            Associations = new HubspotAssociationClient<HubspotAssociationEntity>(httpClient);
        }

        public CRMObjects CRM
        {
            get;
        }

        public HubspotAssociationClient<HubspotAssociationEntity> Associations
        {
            get;
        }
    }
}