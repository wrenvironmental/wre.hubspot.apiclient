using System.Text.Json;
using System.Text.Json.Serialization;
using wre.hubspot.apiclient.CRM.Contacts;
using wre.hubspot.apiclient.CRM.CustomObjects;
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
        settings.Converters.Add(new DateTimeConverter());
        settings.Converters.Add(new PolymorphicWriteOnlyJsonConverter<HubspotCustomObject>());
        settings.Converters.Add(new PolymorphicWriteOnlyJsonConverter<HubspotContact>());

        if (entity is not IHubspotCustomSerialization hubspotEntity)
            return JsonSerializer.Serialize(entity, settings);

        return JsonSerializer.Serialize(hubspotEntity.GetCustomObject(entity), settings);
    }
}

public class PolymorphicWriteOnlyJsonConverter<T> : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}