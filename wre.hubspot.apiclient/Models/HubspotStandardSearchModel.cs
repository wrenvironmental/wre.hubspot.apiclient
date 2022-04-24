using System.Text.Json.Serialization;

namespace wre.hubspot.apiclient.Models;

public class HubspotStandardSearchModel
{
    public HubspotStandardSearchModel(Dictionary<string, dynamic> parameters)
    {
        FilterGroups = new List<HubspotFilterGroup> {new()};
        foreach (var parameter in parameters)
        {
            FilterGroups.First().Filters.Add(new HubspotFilter()
            {
                PropertyName = parameter.Key.ToLower(),
                Operator = "EQ",
                Value = parameter.Value.ToString()
            });
        }
    }

    [JsonPropertyName("filterGroups")]
    public List<HubspotFilterGroup> FilterGroups { get; set; }
}

public class HubspotFilterGroup
{
    public HubspotFilterGroup()
    {
        Filters = new List<HubspotFilter>();
    }

    [JsonPropertyName("filters")]
    public List<HubspotFilter> Filters { get; set; }
}

public class HubspotFilter
{
    [JsonPropertyName("propertyName")]
    public string? PropertyName { get; set; }
    public string? Operator { get; set; }
    public string? Value { get; set; }
}