namespace wre.hubspot.apiclient.Interfaces;

public interface IHubspotCustomEntity
{
    string EntityUrlSuffix { get; }
    string ObjectTypeId { get; set; }
}