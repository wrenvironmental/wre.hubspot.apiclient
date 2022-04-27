namespace wre.hubspot.apiclient.Interfaces;

public interface IHubspotCustomEntity : IHubspotEntity
{
    string ObjectTypeId { get; set; }
}