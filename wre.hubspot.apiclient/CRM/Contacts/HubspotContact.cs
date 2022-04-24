using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Contacts;

public class HubspotContact : HubspotCommonEntity, IHubspotCustomSerialization, IHubspotContact
{
    public HubspotContact() : base("objects/contacts") { }
    public string? Company { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }
}