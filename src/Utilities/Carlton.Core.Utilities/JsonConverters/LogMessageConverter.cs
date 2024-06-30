using System.Text.Json;
using System.Text.Json.Serialization;
namespace Carlton.Core.Utilities.JsonConverters;

/// <summary>
/// Json converter for serializing and deserializing <see cref="System.Exception"/> instances to and from JSON.
/// </summary>
public class LogMessageConverter : JsonConverter<Exception>
{
    /// <summary>
    /// Reads the JSON representation of the object and converts it to a <see cref="System.Exception"/> instance.
    /// </summary>
    /// <param name="reader">The reader to read from.</param>
    /// <param name="typeToConvert">The type of the object to convert.</param>
    /// <param name="options">The serializer options.</param>
    /// <returns>A deserialized <see cref="System.Exception"/> instance.</returns>
    public override Exception Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Implement if needed: Deserialization of Exception from JSON
        throw new NotImplementedException("Deserialization of exceptions not supported.");
    }

    /// <summary>
    /// Writes the JSON representation of the <see cref="System.Exception"/> instance.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The <see cref="System.Exception"/> instance to write.</param>
    /// <param name="options">The serializer options.</param>
    public override void Write(Utf8JsonWriter writer, Exception value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("ExceptionType", value.GetType().FullName);
        writer.WriteString("Message", value.Message);
        writer.WriteString("StackTrace", value.StackTrace);
        writer.WriteEndObject();
    }
}
