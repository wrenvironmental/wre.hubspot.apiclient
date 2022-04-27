using System.Text.Json.Serialization;

namespace wre.hubspot.apiclient.Models;

public class HubspotStandardResponseModel<T>
{
    public long? Id { get; set; }

    [JsonPropertyName("properties")]
    public T? Result { get; set; }
}