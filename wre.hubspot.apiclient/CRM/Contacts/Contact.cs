using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Contacts;

public class Contact : HubspotCommonEntity, IHubspotEntity , IHubspotCustomSerialization
{
    public string Company { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }
    [JsonIgnore]
    public string CreateUrl => "objects/contacts";
    [JsonIgnore]
    public string UpdateUrl => CreateUrl;
}