namespace Carlton.Core.Flux.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class HttpResponsePropertyAttribute(string responseAttributeName) : Attribute
{
    public string ResponsePropertyName { get; init; } = responseAttributeName;
}
