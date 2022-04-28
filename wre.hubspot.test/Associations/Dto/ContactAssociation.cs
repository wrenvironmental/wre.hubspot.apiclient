using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.test.Associations.Dto;

public class ContactAssociation : IHubspotAssociation
{
    public ContactAssociation()
    {
        AssociationId = string.Empty;
    }

    public string AssociationId { get; set; }
    public string EntityName => "contacts";
    public string AssociationName => "contact";
}