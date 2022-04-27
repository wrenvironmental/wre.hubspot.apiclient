using System.Text.Json;
using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Infrastructure;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Extensions;

public static class StringExtensions
{
    public static string SerializeToJson(this object entity, bool checkForCustom = true)
    {
        var settings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new LowerCaseNamingPolicy(),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        if (entity is not IHubspotCustomSerialization hubspotEntity) 
            return JsonSerializer.Serialize(entity, settings);

        
        settings.Converters.Add(new DateTimeConverter());
        return JsonSerializer.Serialize(hubspotEntity.GetCustomObject(entity), settings);

    }
}