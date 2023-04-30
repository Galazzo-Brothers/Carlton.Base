namespace Carlton.Base.State;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DataRefreshEndpointAttribute : Attribute
{
    public HttpVerb HttpVerb { get; init; }
    public string Route { get; init; } 

    public DataRefreshEndpointAttribute(string route)
    {
        Route = route;
    }
}


