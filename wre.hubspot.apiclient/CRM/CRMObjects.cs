using wre.hubspot.apiclient.CRM.Contacts;
using wre.hubspot.apiclient.CRM.CustomObjects;
using wre.hubspot.apiclient.CRM.Deals;
using wre.hubspot.apiclient.CRM.Products;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM;

public class CRMObjects
{
    public CRMObjects(string baseUrl)
    {
        Contacts = new HubspotContactClient<HubspotContact>(baseUrl);
        Deals = new HubspotDealClient<HubspotDeal>(baseUrl);
        Products = new HubspotProductClient<HubspotProduct>(baseUrl);
        CustomObjects = new HubspotCustomObjectClient<HubspotCustomObject>(baseUrl);
    }

    public CRMObjects(HttpClient httpClient)
    {
        Contacts = new HubspotContactClient<HubspotContact>(httpClient);
        Deals = new HubspotDealClient<HubspotDeal>(httpClient);
        Products = new HubspotProductClient<HubspotProduct>(httpClient);
        CustomObjects = new HubspotCustomObjectClient<HubspotCustomObject>(httpClient);
    }

    public HubspotContactClient<HubspotContact> Contacts { get; set; }
    public HubspotDealClient<HubspotDeal> Deals { get; set; }
    public HubspotProductClient<HubspotProduct> Products { get; set; }
    public HubspotCustomObjectClient<HubspotCustomObject> CustomObjects { get; set; }
}