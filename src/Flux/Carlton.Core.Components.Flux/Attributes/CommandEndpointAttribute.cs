namespace Carlton.Core.Components.Flux;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class CommandEndpointAttribute : Attribute
{
    public HttpVerb HttpVerb { get; init; }
    public string Route { get; init; }

    public CommandEndpointAttribute(string route)
    {
        Route = route;
    }
}
