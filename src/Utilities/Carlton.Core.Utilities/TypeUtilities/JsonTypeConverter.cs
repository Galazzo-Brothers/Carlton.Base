namespace Carlton.Core.Utilities.TypeUtilities;

public class JsonTypeConverter : JsonConverter<Type>
{
    public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string typeName = reader.GetString();
        return Type.GetType(typeName);
    }

    public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
    {
        if (value is not null)
        {
            writer.WriteStringValue(value.AssemblyQualifiedName);
        }
    }
}

