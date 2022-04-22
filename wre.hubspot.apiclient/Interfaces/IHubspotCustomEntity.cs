namespace wre.hubspot.apiclient.Interfaces;

public interface IHubspotCustomEntity
{
    string EntityUrlPrefix { get; }
    string ObjectTypeId { get; set; }
}