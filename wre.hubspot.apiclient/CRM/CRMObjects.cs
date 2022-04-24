using wre.hubspot.apiclient.CRM.Contacts;
using wre.hubspot.apiclient.CRM.CustomObjects;
using wre.hubspot.apiclient.CRM.Deals;
using wre.hubspot.apiclient.CRM.Products;

namespace wre.hubspot.apiclient.CRM;

public class CRMObjects
{
    public CRMObjects(string baseUrl)
    {
        Contacts = new HubspotContactClient<HubspotContact>(baseUrl);
        Deals = new HubspotDealClient<HubspotDeal>(baseUrl);
        Products = new ProductClient(baseUrl);
        CustomObjects = new CustomObjectClient(baseUrl);
    }

    public HubspotContactClient<HubspotContact> Contacts { get; set; }
    public HubspotDealClient<HubspotDeal> Deals { get; set; }
    public ProductClient Products { get; set; }
    public CustomObjectClient CustomObjects { get; set; }
}