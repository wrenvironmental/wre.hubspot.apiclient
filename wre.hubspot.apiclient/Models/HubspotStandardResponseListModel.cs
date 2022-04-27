using System.Text.Json.Serialization;

namespace wre.hubspot.apiclient.Models;

public class HubspotStandardResponseListModel<T>
{
    [JsonPropertyName("results")]
    public List<HubspotStandardResponseModel<T>>? Result { get; set; }
}