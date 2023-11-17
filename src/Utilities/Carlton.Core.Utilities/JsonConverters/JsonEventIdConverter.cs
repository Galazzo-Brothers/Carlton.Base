namespace Carlton.Core.Utilities.JsonConverters;

public class JsonEventIdConverter : JsonConverter<EventId>
{
    public override EventId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        // Implement deserialization logic here
        var root = doc.RootElement;
        int id = root.GetProperty("Id").GetInt32();
        string name = root.GetProperty("Name").GetString();
        return new EventId(id, name);
    }

    public override void Write(Utf8JsonWriter writer, EventId value, JsonSerializerOptions options)
    {
        // Implement serialization logic here
        writer.WriteStartObject();
        writer.WriteNumber("Id", value.Id);
        writer.WriteString("Name", value.Name);
        writer.WriteEndObject();
    }
}