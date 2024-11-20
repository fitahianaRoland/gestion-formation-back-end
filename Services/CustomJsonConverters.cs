using System.Text.Json;
using System.Text.Json.Serialization;

namespace GestionFormation.Services
{
    public class CustomJsonConverters
    {
        // Convertisseur pour TimeOnly (format HH:mm)
        public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
        {
            private const string Format = "HH:mm";

            public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (TimeOnly.TryParseExact(reader.GetString(), Format, out var time))
                {
                    return time;
                }
                throw new JsonException($"Invalid time format. Expected '{Format}'");
            }

            public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(Format));
            }
        }
    }
}
