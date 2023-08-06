namespace Carlton.Core.Components.Flux;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ViewModelEndpointAttribute : Attribute
{
    public HttpVerb HttpVerb { get; init; }
    public string Route { get; init; } 

    public ViewModelEndpointAttribute(string route)
    {
        Route = route;
    }
}


