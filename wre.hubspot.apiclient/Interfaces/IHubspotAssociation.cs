namespace wre.hubspot.apiclient.Interfaces;

public interface IHubspotAssociation
{
    string Id { get; set; }
    string EntityName { get; }
    string AssociationName { get; }
}