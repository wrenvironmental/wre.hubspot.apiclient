using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Associations;

namespace wre.hubspot.apiclient.Models;

public class HubspotStandardResponseModel<T>
{
    public long? Id { get; set; }

    [JsonPropertyName("properties")]
    public T? Result { get; set; }
}

public class HubspotStandardResponseModel
{
    public long? Id { get; set; }

    public string Type { get; set; } = null!;
}

public class HubspotStandardAssociationResponseModel
{
    public Identifier From { get; set; } = null!;
    public Identifier To { get; set; } = null!;
    public string Type { get; set; } = null!;
}
