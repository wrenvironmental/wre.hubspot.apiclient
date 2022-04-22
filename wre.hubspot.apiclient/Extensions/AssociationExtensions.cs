using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Extensions;

public static class AssociationExtensions
{
    public static HubspotAssociation Associate(this IHubspotClient client, IHubspotAssociation fromEntity)
    {
        return new HubspotAssociation(fromEntity, client);
    }

    public static Task WithAsync(this HubspotAssociation association, IHubspotAssociation toEntity)
    {
        var entity = association.GetEntity(toEntity);
        return association.GetClient().CreateAsync(entity);
    }
}