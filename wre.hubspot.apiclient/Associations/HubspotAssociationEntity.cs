using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Associations;

public class HubspotAssociationEntity : IHubspotEntity
{
    private readonly string _fromEntity;
    private readonly string _toEntity;
    /// <summary>
    /// Not used
    /// </summary>
    public long? Id { get; set; }

    public HubspotAssociationEntity(IHubspotAssociation from, IHubspotAssociation to)
    {
        _fromEntity = from.EntityName;
        _toEntity = to.EntityName;
        From = new Identifier(from.AssociationId);
        To = new Identifier(to.AssociationId);
        Type = $"{from.AssociationName}_to_{to.AssociationName}";
    }

    public Identifier From { get; set; }
    public Identifier To { get; set; }
    public string Type { get; set; }

    public string EntityUrlSuffix => $"{_fromEntity}/{_toEntity}";
}

public class Identifier
{
    public Identifier(string? id)
    {
        Id = id;
    }
    public string? Id { get; set; }
}