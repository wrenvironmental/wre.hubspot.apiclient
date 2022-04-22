using System.Text.Json.Serialization;

namespace wre.hubspot.apiclient.Common;

public class HubspotStandardResponseModel<T>
{
    public int? Id { get; set; }

    [JsonPropertyName("properties")]
    public T? Result { get; set; }
}