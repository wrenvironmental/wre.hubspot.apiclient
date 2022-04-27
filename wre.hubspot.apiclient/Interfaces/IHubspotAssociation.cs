namespace wre.hubspot.apiclient.Interfaces;

public interface IHubspotAssociation
{
    string AssociationId { get; set; }
    string EntityName { get; }
    string AssociationName { get; }
}