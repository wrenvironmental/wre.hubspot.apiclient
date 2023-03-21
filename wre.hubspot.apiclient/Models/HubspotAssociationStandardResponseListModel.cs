using System.Text.Json.Serialization;

namespace wre.hubspot.apiclient.Models
{
    public class HubspotAssociationStandardResponseListModel
    {
        [JsonPropertyName("results")]
        public List<HubspotStandardResponseModel>? Result { get; set; }
    }
}
