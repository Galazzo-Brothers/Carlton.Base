namespace Carlton.Base.State;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DataEndpointAttribute : Attribute
{
    public HttpVerb HttpVerb { get; init; }
    public string Route { get; init; } 

    public DataEndpointAttribute(string route)
    {
        Route = route;
    }
}


