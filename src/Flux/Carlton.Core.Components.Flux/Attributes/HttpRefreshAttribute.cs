using Carlton.Core.Components.Flux.Attributes;

namespace Carlton.Core.Components.Flux;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ViewModelHttpRefreshAttribute: HttpRefreshAttribute
{
    public ViewModelHttpRefreshAttribute(string route)
        : base(route, HttpVerb.GET, DataEndpointRefreshPolicy.Always)
        => Route = route;
}


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class MutationHttpRefreshAttribute : HttpRefreshAttribute
{
    public MutationHttpRefreshAttribute(string route)
     : base(route, HttpVerb.POST, DataEndpointRefreshPolicy.Always)
        => Route = route;
}


public abstract class HttpRefreshAttribute : Attribute
{
    public string Route { get; init; }
    public HttpVerb HttpVerb { get; set; }
    public DataEndpointRefreshPolicy DataRefreshPolicy { get; set; }

    public HttpRefreshAttribute(string route, HttpVerb httpVerb, DataEndpointRefreshPolicy dataRefreshPolicy)
        => (Route, HttpVerb, DataRefreshPolicy) = (route, httpVerb, dataRefreshPolicy);
}
