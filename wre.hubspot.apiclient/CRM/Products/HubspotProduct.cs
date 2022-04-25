using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Products;

public class HubspotProduct : HubspotCommonEntity, IHubspotCustomSerialization, IHubspotProduct
{
    public HubspotProduct() : base("objects/products") { }
    public string? Description { get; set; }
    [JsonPropertyName("hs_sku")]
    public string? Code { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}