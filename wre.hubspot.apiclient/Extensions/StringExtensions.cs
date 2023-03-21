using Flurl;
using System.Text.Json;
using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Infrastructure;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Extensions;

public static class StringExtensions
{
    public static string SerializeToJson(this object entity)
    {
        var settings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new LowerCaseNamingPolicy(),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        settings.Converters.Add(new DateTimeConverter());

        if (entity is not IHubspotCustomSerialization hubspotEntity)
            return JsonSerializer.Serialize(entity, settings);

        return JsonSerializer.Serialize(hubspotEntity.GetCustomObject(entity), settings);
    }

    public static string GetFullUrl(this IHubspotEntity entity, string baseUrlPrefix, bool isSearchUrl = false)
    {
        if (entity is IHubspotCustomEntity custom)
        {
            return Url.Combine(baseUrlPrefix, entity.EntityUrlSuffix, custom.ObjectTypeId, isSearchUrl ? "search" : string.Empty);
        }
        return Url.Combine(baseUrlPrefix, entity.EntityUrlSuffix, isSearchUrl ? "search" : string.Empty);
    }
}