using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace PCL.Domain.Utils
{
    public class TimeSpanToStringConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (TimeSpan.TryParse(value, out var result))
                {
                    return result;
                }
                throw new JsonException("Invalid TimeSpan format.");
            }
            else if (reader.TokenType == JsonTokenType.StartObject)
            {
                // Lendo objeto JSON
                using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
                {
                    var ticksProperty = doc.RootElement.GetProperty("ticks");
                    if (ticksProperty.ValueKind != JsonValueKind.Number)
                    {
                        throw new JsonException("Ticks value is not a number.");
                    }
                    var ticks = ticksProperty.GetInt64();
                    return TimeSpan.FromTicks(ticks);
                }
            }

            throw new JsonException($"Unexpected token parsing TimeSpan. Expected String or Object, got {reader.TokenType}.");
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(@"hh\:mm\:ss"));
        }
    }
}
