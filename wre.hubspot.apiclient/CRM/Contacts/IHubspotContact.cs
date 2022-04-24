using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Contacts;

public interface IHubspotContact : IHubspotEntity
{
    public string? Company { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }
}