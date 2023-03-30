using Flurl;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.CRM.Companies;
using wre.hubspot.apiclient.CRM.Contacts;
using wre.hubspot.apiclient.CRM.Deals;
using wre.hubspot.apiclient.Infrastructure;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Extensions;

public static class StringExtensions
{
    public static string SerializeToJson(this object entity)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new LowerCaseNamingPolicy(),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        options.Converters.Add(new DateTimeConverter());

        if (entity is not IHubspotCustomSerialization hubspotEntity)
        {
            //settings.Converters.Add(new PolymorphicWriteOnlyJsonConverter<HubspotCompany>());
            //settings.Converters.Add(new PolymorphicWriteOnlyJsonConverter<HubspotContact>());
            //options.Converters.Add(new PolymorphicWriteOnlyJsonConverter<HubspotDeal>());

            return JsonSerializer.Serialize(entity, options);
        }

        return JsonSerializer.Serialize(hubspotEntity.GetCustomObject(entity), options);
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

public class PolymorphicWriteOnlyJsonConverter<T> : JsonConverter<T>
{
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        reader.Skip();
        return default(T);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}