using System.Text.Json.Serialization;

namespace wre.hubspot.apiclient.Models;

public class HubspotStandardRequestModel<T>
{
    public HubspotStandardRequestModel(T entity)
    {
        RequestObject = entity;
    }

    [JsonPropertyName("properties")]
    public T RequestObject { get; }
}