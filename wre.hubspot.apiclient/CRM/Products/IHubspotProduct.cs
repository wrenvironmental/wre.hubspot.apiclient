using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Products;

public interface IHubspotProduct : IHubspotEntity
{
    public string? Description { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}