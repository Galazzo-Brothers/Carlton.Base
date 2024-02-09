namespace Carlton.Core.Utilities.JsonConverters;

public class LogMessageConverter : JsonConverter<Exception>
{
    public override Exception Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Implement if needed: Deserialization of Exception from JSON
        throw new NotImplementedException("Deserialization of exceptions not supported.");
    }

    public override void Write(Utf8JsonWriter writer, Exception value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("ExceptionType", value.GetType().FullName);
        writer.WriteString("Message", value.Message);
        writer.WriteString("StackTrace", value.StackTrace);
        writer.WriteEndObject();
    }
}
