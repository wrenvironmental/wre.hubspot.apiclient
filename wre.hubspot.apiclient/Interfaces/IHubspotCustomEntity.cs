namespace wre.hubspot.apiclient.Interfaces;

public interface IHubspotCustomEntity
{
    string CreateUrl { get; }
    string UpdateUrl { get; }
    string ObjectTypeId { get; set; }
}