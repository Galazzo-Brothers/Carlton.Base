using Carlton.Core.Utilities.Extensions;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Carlton.Core.Utilities.TypeUtilities;

public class MyCustomConverter : JsonConverter<Type>
{
    public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Implement custom deserialization logic here
        // You need to read the JSON data from the reader and create an instance of MyType.
        // For example:

        using (var doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            
            return new Type()
            {
                FirstName = root.GetProperty("first_name").GetString(),
                LastName = root.GetProperty("last_name").GetString(),
                Age = root.GetProperty("age").GetInt32()
            };
        }


        reader.Read();
        var value = reader.GetString();
        // return new MyType(value);

        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("typeDisplayName");
        writer.WriteStringValue(value.GetDisplayName());
        writer.WritePropertyName("typeString");
        writer.WriteStringValue(value.ToString());
        writer.WriteEndObject();
    }
}

