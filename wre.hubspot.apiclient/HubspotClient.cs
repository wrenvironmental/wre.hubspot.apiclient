using wre.hubspot.apiclient.CRM;
// ReSharper disable InconsistentNaming

namespace wre.hubspot.apiclient
{
    public class HubspotClient
    {
        public HubspotClient()
        {
            var baseUrl = HubspotSettings.BaseUrl ?? throw new ArgumentException(nameof(HubspotSettings.BaseUrl));
            CRM = new CRMObjects(baseUrl);
        }

        public CRMObjects CRM
        {
            get;
        }
    }
}