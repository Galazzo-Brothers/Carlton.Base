using Carlton.Core.Components.Flux.Attributes;

namespace Carlton.Core.Components.Flux;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ViewModelHttpRefreshAttribute: Attribute
{
    public string Route { get; init; }
    public HttpVerb HttpVerb { get; set; } = HttpVerb.GET;
    public DataEndpointRefreshPolicy DataRefreshPolicy { get; set; } = DataEndpointRefreshPolicy.Always;

    public ViewModelHttpRefreshAttribute(string route)
        => Route = route;
}
