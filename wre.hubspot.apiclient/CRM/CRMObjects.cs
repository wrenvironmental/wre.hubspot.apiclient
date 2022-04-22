using wre.hubspot.apiclient.CRM.Contacts;
using wre.hubspot.apiclient.CRM.CustomObjects;
using wre.hubspot.apiclient.CRM.Deals;
using wre.hubspot.apiclient.CRM.Products;

namespace wre.hubspot.apiclient.CRM;

public class CRMObjects
{
    public CRMObjects(string baseUrl)
    {
        Contacts = new ContactClient(baseUrl);
        Deals = new DealClient(baseUrl);
        Products = new ProductClient(baseUrl);
        CustomObjects = new CustomObjectClient(baseUrl);
    }

    public ContactClient Contacts { get; set; }
    public DealClient Deals { get; set; }
    public ProductClient Products { get; set; }
    public CustomObjectClient CustomObjects { get; set; }
}