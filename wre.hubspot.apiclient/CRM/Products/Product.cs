using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;
// ReSharper disable UnusedMember.Global

namespace wre.hubspot.apiclient.CRM.Products;

public class Product : HubspotCommonEntity, IHubspotEntity , IHubspotCustomSerialization
{
    public string Description { get; set; }
    [JsonPropertyName("hs_sku")]
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    [JsonIgnore]
    public string CreateUrl => "objects/products";
    [JsonIgnore]
    public string UpdateUrl => CreateUrl;
}