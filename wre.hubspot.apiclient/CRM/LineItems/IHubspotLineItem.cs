using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.LineItems;

public interface IHubspotLineItem : IHubspotEntity
{
    decimal Price { get; set; }
    decimal Discount { get; set; }
    decimal Quantity { get; set; }
    string? Name { get; set; }
    int? ProductId { get; set; }
}