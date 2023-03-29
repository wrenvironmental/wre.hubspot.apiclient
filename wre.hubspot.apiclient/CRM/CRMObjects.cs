using wre.hubspot.apiclient.CRM.Companies;
using wre.hubspot.apiclient.CRM.Contacts;
using wre.hubspot.apiclient.CRM.CustomObjects;
using wre.hubspot.apiclient.CRM.Deals;
using wre.hubspot.apiclient.CRM.LineItems;
using wre.hubspot.apiclient.CRM.Products;

namespace wre.hubspot.apiclient.CRM;

public class CRMObjects
{
    public CRMObjects(string baseUrl, string apiKey)
    {
        Companies = new HubspotCompanyClient<HubspotCompany>(baseUrl, apiKey);
        Contacts = new HubspotContactClient<HubspotContact>(baseUrl, apiKey);
        CustomObjects = new HubspotCustomObjectClient<HubspotCustomObject>(baseUrl, apiKey);
        Deals = new HubspotDealClient<HubspotDeal>(baseUrl, apiKey);
        LineItems = new HubspotLineItemClient<HubspotLineItem>(baseUrl, apiKey);
        Products = new HubspotProductClient<HubspotProduct>(baseUrl, apiKey);
    }

    public CRMObjects(HttpClient httpClient)
    {
        Companies = new HubspotCompanyClient<HubspotCompany>(httpClient);
        Contacts = new HubspotContactClient<HubspotContact>(httpClient);
        CustomObjects = new HubspotCustomObjectClient<HubspotCustomObject>(httpClient);
        Deals = new HubspotDealClient<HubspotDeal>(httpClient);
        LineItems = new HubspotLineItemClient<HubspotLineItem>(httpClient);
        Products = new HubspotProductClient<HubspotProduct>(httpClient);        
    }

    public HubspotCompanyClient<HubspotCompany> Companies { get; set; }
    public HubspotContactClient<HubspotContact> Contacts { get; set; }
    public HubspotCustomObjectClient<HubspotCustomObject> CustomObjects { get; set; }
    public HubspotDealClient<HubspotDeal> Deals { get; set; }
    public HubspotLineItemClient<HubspotLineItem> LineItems { get; set; }
    public HubspotProductClient<HubspotProduct> Products { get; set; }
    
}