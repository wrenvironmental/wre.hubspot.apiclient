using System.Text.Json.Serialization;

namespace wre.hubspot.test.Company.Dto
{
    public class CustomCompany : apiclient.CRM.Companies.HubspotCompany
    {
        [JsonPropertyName("gallons")]
        public int Gallons { get; set; }
        public int JobsiteId { get; set; }
    }
}
