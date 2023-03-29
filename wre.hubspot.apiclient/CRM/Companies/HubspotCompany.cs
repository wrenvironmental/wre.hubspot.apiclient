using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Companies
{
    public class HubspotCompany : HubspotCommonEntity, IHubspotCustomSerialization, IHubspotCompany
    {
        public HubspotCompany() : base("objects/companies") { }
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        [JsonPropertyName("contact_name")]
        public string? Contact { get; set; }
        public string? Address { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
    }
}
