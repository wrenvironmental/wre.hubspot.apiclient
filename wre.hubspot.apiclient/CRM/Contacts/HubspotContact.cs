﻿using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.Contacts;

public class HubspotContact : HubspotCommonEntity, IHubspotCustomSerialization, IHubspotContact
{
    public HubspotContact() : base("objects/contacts")
    {
    }

    public long? Id { get; set; }
    public string? Company { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Zip { get; set; }

    #region Remove

    public int CustId { get; set; }

    [JsonPropertyName("accstatus")]
    public string AccountStatus { get; set; }

    [JsonPropertyName("mcreateddate")]
    public DateTime CreatedDate { get; set; }

    #endregion Remove
}