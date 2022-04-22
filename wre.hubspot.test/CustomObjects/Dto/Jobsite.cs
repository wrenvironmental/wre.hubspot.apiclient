using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using wre.hubspot.apiclient.CRM.CustomObjects;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.test.CustomObjects.Dto;

public class Jobsite : CustomObject, IHubspotCustomEntity, IHubspotCustomSerialization
{
    public Jobsite()
    {
        ObjectTypeId = "2-6130903";
    }

    [Key]
    [JsonPropertyName("jobsiteid")]
    public int JobsiteId { get; set; }
    [JsonPropertyName("sitename")]
    public string SiteName { get; set; }
    [JsonPropertyName("contactname")]
    public string ContactName { get; set; }
    [JsonPropertyName("address")]
    public string Address { get; set; }
    [JsonPropertyName("city")]
    public string City { get; set; }
    [JsonPropertyName("state")]
    public string State { get; set; }
    [JsonPropertyName("zip")]
    public string Zip { get; set; }
    [JsonPropertyName("gallons")]
    public int? Gallons { get; set; }
    [JsonPropertyName("lastservicedate")]
    public DateTime? LastServiceDate { get; set; }
    [JsonPropertyName("nextservicedate")]
    public DateTime? NextServiceDate { get; set; }
    [JsonPropertyName("acquisitionname")]
    public string AcquisitionName { get; set; }
    [JsonIgnore]
    public string CreateUrl => "objects";
    [JsonIgnore]
    public string UpdateUrl => CreateUrl;
    [JsonIgnore]
    public string ObjectTypeId { get; set; }
}