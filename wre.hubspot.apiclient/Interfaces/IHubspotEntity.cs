namespace wre.hubspot.apiclient.Interfaces;

public interface IHubspotEntity
{
    long? Id { get; set; }
    string EntityUrlSuffix { get; }
}