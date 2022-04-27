using System.Text.Json.Serialization;

namespace wre.hubspot.apiclient.Associations;

public class HubspotStandardAssociationRequestModel<T>
{
    public HubspotStandardAssociationRequestModel()
    {
        Mappings = new List<T>();
    }

    [JsonPropertyName("inputs")]
    public List<T> Mappings { get; }
}