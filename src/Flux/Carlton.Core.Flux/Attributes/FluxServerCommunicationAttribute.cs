namespace Carlton.Core.Flux.Attributes;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public partial class FluxServerCommunicationAttribute : Attribute
{
    public string ServerUrl { get; init; }
    public HttpVerb HttpVerb { get; init; }
    public FluxServerCommunicationPolicy ServerCommunicationPolicy { get; init; }

    public FluxServerCommunicationAttribute(string serverUrl, HttpVerb httpVerb, FluxServerCommunicationPolicy serverCommunicationPolicy)
        =>  (ServerUrl, HttpVerb, ServerCommunicationPolicy) = (serverUrl, httpVerb, serverCommunicationPolicy);
}

