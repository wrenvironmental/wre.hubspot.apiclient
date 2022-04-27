using System;
using System.Text.Json.Serialization;

namespace wre.hubspot.test.CustomObjects.Dto;

public class MyCustomObject : apiclient.CRM.CustomObjects.HubspotCustomObject
{
    public MyCustomObject()
    {
        ObjectTypeId = "2-6341115";
    }

    [JsonPropertyName("jobsiteid")]
    public int JobsiteId { get; set; }
    [JsonPropertyName("sitename")]
    public string? SiteName { get; set; }
    [JsonPropertyName("contactname")]
    public string? ContactName { get; set; }
    [JsonPropertyName("address")]
    public string? Address { get; set; }
    [JsonPropertyName("city")]
    public string? City { get; set; }
    [JsonPropertyName("state")]
    public string? State { get; set; }
    [JsonPropertyName("zip")]
    public string? Zip { get; set; }
    [JsonPropertyName("gallons")]
    public int? Gallons { get; set; }
    [JsonPropertyName("lastservicedate")]
    public DateTime? LastServiceDate { get; set; }
    [JsonPropertyName("nextservicedate")]
    public DateTime? NextServiceDate { get; set; }
    [JsonPropertyName("acquisitionname")]
    public string? AcquisitionName { get; set; }
}