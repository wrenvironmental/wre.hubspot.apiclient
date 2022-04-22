using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Associations;

public class HubspotAssociationEntity : IHubspotEntity, IHubspotCustomSerialization
{
    private readonly string _fromEntity;
    private readonly string _toEntity;

    public HubspotAssociationEntity(IHubspotAssociation from, IHubspotAssociation to)
    {
        _fromEntity = from.EntityName;
        _toEntity = to.EntityName;
        From = new Identifier(from.Id);
        To = new Identifier(to.Id);
        Type = $"{from.AssociationName}_to_{to.AssociationName}";
    }

    public Identifier From { get; set; }
    public Identifier To { get; set; }
    public string Type { get; set; }

    public string EntityUrlSuffix => $"{_fromEntity}/{_toEntity}/batch/create";
    public object GetCustomObject<T>(T entity)
    {
        return new HubspotStandardAssociationRequestModel<T>
        {
            Mappings = { entity }
        };
    }
}

public class Identifier
{
    public Identifier(string id)
    {
        Id = id;
    }
    public string Id { get; set; }
}