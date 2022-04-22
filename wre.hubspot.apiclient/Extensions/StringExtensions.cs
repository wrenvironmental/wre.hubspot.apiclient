using System.Text.Json;
using wre.hubspot.apiclient.Infrastructure;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Extensions;

public static class StringExtensions
{
    public static string SerializeToJson(this object entity)
    {
        if (entity is not IHubspotCustomSerialization hubspotEntity) return JsonSerializer.Serialize(entity);

        var settings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new LowerCaseNamingPolicy()
        };
        settings.Converters.Add(new DateTimeConverter());
        return JsonSerializer.Serialize(hubspotEntity.GetCustomObject(entity), settings);

    }
}