using System.Text.Json;
using System.Text.Json.Serialization;
namespace Carlton.Core.Utilities.JsonConverters;

/// <summary>
/// Json converter for converting <see cref="System.Type"/> instances to and from JSON.
/// </summary>
public class JsonTypeConverter : JsonConverter<Type>
{
    /// <summary>
    /// Reads the JSON representation of the object and converts it to a <see cref="System.Type"/> instance.
    /// </summary>
    /// <param name="reader">The reader to read from.</param>
    /// <param name="typeToConvert">The type of the object to convert.</param>
    /// <param name="options">The serializer options.</param>
    /// <returns>The deserialized <see cref="System.Type"/> instance.</returns>
    public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string typeName = reader.GetString();
        return Type.GetType(typeName);
    }

    /// <summary>
    /// Writes the JSON representation of the <see cref="System.Type"/> instance.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The <see cref="System.Type"/> instance to write.</param>
    /// <param name="options">The serializer options.</param>
    public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
    {
        if (value is not null)
        {
            writer.WriteStringValue(value.AssemblyQualifiedName);
        }
    }
}

