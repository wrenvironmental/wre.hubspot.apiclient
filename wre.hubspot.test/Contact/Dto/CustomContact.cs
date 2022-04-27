using System.Text.Json.Serialization;

namespace wre.hubspot.test.Contact.Dto;

public class CustomContact : wre.hubspot.apiclient.CRM.Contacts.HubspotContact
{
    [JsonPropertyName("custid")]
    public int CustomerId { get; set; }
}