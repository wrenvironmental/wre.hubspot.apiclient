using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.CustomObjects;

public class HubspotCustomObject : HubspotCommonEntity, IHubspotCustomSerialization, IHubspotCustomEntity
{
    public HubspotCustomObject() : base("objects")
    {
        
    }
    [JsonIgnore]
    public string ObjectTypeId { get; set; } = string.Empty;
}