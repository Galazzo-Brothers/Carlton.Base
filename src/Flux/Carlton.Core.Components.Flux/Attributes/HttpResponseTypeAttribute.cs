namespace Carlton.Core.Flux.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class HttpResponseTypeAttribute<TResponse> : Attribute
{
}
