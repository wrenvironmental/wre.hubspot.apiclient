namespace wre.hubspot.apiclient.Common;

public abstract class HubspotCommonEntity
{
    public virtual object GetCustomObject<T>(T entity)
    {
        return new HubspotStandardRequestModel<T>(entity);
    }
}