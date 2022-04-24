using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Models;

namespace wre.hubspot.apiclient.Common;

public abstract class HubspotCommonEntity
{
    [JsonIgnore]
    public string EntityUrlSuffix { get; }

    protected HubspotCommonEntity(string entityUrlPrefix)
    {
        EntityUrlSuffix = entityUrlPrefix;
    }

    public virtual object GetCustomObject<T>(T entity)
    {
        return new HubspotStandardRequestModel<T>(entity);
    }
}