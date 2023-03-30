using System.Text.Json;
using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Extensions;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Models;

public class HubspotStandardRequestModel<T>
{
    public long? Id { get; set; }

    public HubspotStandardRequestModel(T entity)
    {
        if (entity != null)
        {
            RequestObject = JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(entity));
            if (RequestObject is IHubspotEntity hEntity)
            {
                //When sending data to Hubspot, the object cannot contain an ID
                if (hEntity.Id.HasValue)
                {
                    this.Id = hEntity.Id;
                    hEntity.Id = null;
                }
            }
        }
    }

    [JsonPropertyName("properties")]
    public T? RequestObject { get; }
}