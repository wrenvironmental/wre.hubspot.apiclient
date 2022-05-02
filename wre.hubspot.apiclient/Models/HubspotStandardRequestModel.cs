using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Models;

public class HubspotStandardRequestModel<T>
{
    public long? Id { get; set; }

    public HubspotStandardRequestModel(T entity)
    {
        RequestObject = entity;
        if (entity is IHubspotEntity hEntity)
        {
            //When sending data to Hubspot, the object cannot contain an ID
            if (hEntity.Id.HasValue)
            {
                this.Id = hEntity.Id;
                hEntity.Id = null;
            }
        }
    }

    [JsonPropertyName("properties")]
    public T RequestObject { get; }
}