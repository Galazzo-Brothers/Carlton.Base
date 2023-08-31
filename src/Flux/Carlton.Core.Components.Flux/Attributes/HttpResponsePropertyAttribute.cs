namespace Carlton.Core.Components.Flux.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class HttpResponsePropertyAttribute : Attribute
{
    public string ResponsePropertyName { get; init; }

    public HttpResponsePropertyAttribute(string responseAttributeName)
        => ResponsePropertyName = responseAttributeName;
}
