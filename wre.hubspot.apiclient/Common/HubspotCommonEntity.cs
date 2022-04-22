using System.Text.Json.Serialization;

namespace wre.hubspot.apiclient.Common;

public abstract class HubspotCommonEntity
{
    [JsonIgnore]
    public string EntityUrlPrefix { get; }

    protected HubspotCommonEntity(string entityUrlPrefix)
    {
        EntityUrlPrefix = entityUrlPrefix;
    }

    public virtual object GetCustomObject<T>(T entity)
    {
        return new HubspotStandardRequestModel<T>(entity);
    }
}