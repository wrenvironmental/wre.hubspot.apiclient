using System.Text.Json;
using System.Text.Json.Serialization;

namespace wre.hubspot.apiclient.Infrastructure;

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetDateTime().ToUniversalTime();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        if (value.TimeOfDay.TotalSeconds >= 0)
        {
            //Returning midnight
            writer.WriteStringValue(new DateTime(value.Ticks, DateTimeKind.Utc));
            return;
        }
        writer.WriteStringValue(value.ToUniversalTime());
    }
}