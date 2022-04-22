using System.Text.Json.Serialization;

namespace wre.hubspot.test.Contacts.Dto;

public class CustomDeal : wre.hubspot.apiclient.CRM.Contacts.Contact
{
    [JsonPropertyName("custid")]
    public int CustomerId { get; set; }
}