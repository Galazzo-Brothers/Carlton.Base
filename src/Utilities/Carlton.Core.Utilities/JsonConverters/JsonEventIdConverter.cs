using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
namespace Carlton.Core.Utilities.JsonConverters;

/// <summary>
/// Json converter for converting <see cref="EventId"/> instances to and from JSON.
/// </summary>
public class JsonEventIdConverter : JsonConverter<EventId>
{
    /// <summary>
    /// Reads the JSON representation of the object and converts it to an <see cref="EventId"/> instance.
    /// </summary>
    /// <param name="reader">The reader to read from.</param>
    /// <param name="typeToConvert">The type of the object to convert.</param>
    /// <param name="options">The serializer options.</param>
    /// <returns>The deserialized <see cref="EventId"/> instance.</returns>
    public override EventId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        int id = root.GetProperty("Id").GetInt32();
        string name = root.GetProperty("Name").GetString();
        return new EventId(id, name);
    }

    /// <summary>
    /// Writes the JSON representation of the <see cref="EventId"/> instance.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The <see cref="EventId"/> instance to write.</param>
    /// <param name="options">The serializer options.</param>
    public override void Write(Utf8JsonWriter writer, EventId value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("Id", value.Id);
        writer.WriteString("Name", value.Name);
        writer.WriteEndObject();
    }
}