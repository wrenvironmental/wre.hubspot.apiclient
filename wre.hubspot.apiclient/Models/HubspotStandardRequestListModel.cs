using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Models;

public class HubspotStandardRequestListModel<T>
{
    public HubspotStandardRequestListModel(List<T> entities)
    {
        RequestObjects = new List<object>();

        foreach (var entity in entities)
        {
            if (entity is IHubspotCustomSerialization customSerializableObj)
            {
                RequestObjects.Add(customSerializableObj.GetCustomObject(entity));
            }
            else
            {
                RequestObjects.Add(entity);
            }
        }
    }

    [JsonPropertyName("inputs")]
    public List<object> RequestObjects { get; }
}