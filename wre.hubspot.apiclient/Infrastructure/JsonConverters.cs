using System.Text.Json;
using System.Text.Json.Serialization;

namespace wre.hubspot.apiclient.Infrastructure;

public class NullToEmptyStringConverter : JsonConverter<string?>
{
    public override bool HandleNull => true;

    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value ?? string.Empty);
    }
}

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

public class NullToEmptyDateTimeConverter : JsonConverter<DateTime?>
{
    public override bool HandleNull => true;

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var @string = reader.GetString();
        if (string.IsNullOrWhiteSpace(@string))
        {
            return null;
        }
        return DateTime.Parse(@string);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? date, JsonSerializerOptions options)
    {
        if (date.HasValue)
        {
            if (date.Value.TimeOfDay.TotalSeconds >= 0)
            {
                //Returning midnight
                writer.WriteStringValue(new DateTime(date.Value.Ticks, DateTimeKind.Utc));
                return;
            }
            writer.WriteStringValue(date.Value.ToString("yyyy-MM-dd"));
        }
        else
            writer.WriteStringValue(string.Empty);
    }
}

public class NullToEmptyIntConverter : JsonConverter<int?>
{
    public override bool HandleNull => true;

    public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var @string = reader.GetString();
        if (string.IsNullOrWhiteSpace(@string))
        {
            return null;
        }
        return int.Parse(@string);
    }

    public override void Write(Utf8JsonWriter writer, int? number, JsonSerializerOptions options)
    {
        if (number.HasValue)
        {
            writer.WriteNumberValue(number.Value);
        }
        else
            writer.WriteStringValue(string.Empty);
    }
}