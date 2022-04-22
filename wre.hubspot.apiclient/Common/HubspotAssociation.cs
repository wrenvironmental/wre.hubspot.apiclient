using wre.hubspot.apiclient.Associations;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Common;

public class HubspotAssociation
{
    private readonly IHubspotAssociation _fromEntity;
    private readonly IHubspotClient _client;

    public HubspotAssociation(IHubspotAssociation fromEntity, IHubspotClient client)
    {
        _fromEntity = fromEntity;
        _client = client;
    }

    public IHubspotEntity GetEntity(IHubspotAssociation toEntity)
    {
        return new HubspotAssociationEntity(_fromEntity, toEntity);
    }

    public IHubspotClient GetClient()
    {
        return _client;
    }
}