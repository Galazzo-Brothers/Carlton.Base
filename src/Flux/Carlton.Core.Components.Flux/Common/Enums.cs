namespace Carlton.Core.Components.Flux;

public enum DataEndpointParameterType
{
    StateStoreParameter,
    ComponentParameter
}

public enum HttpVerb
{
    GET,
    POST,
    PUT
}

public enum DataEndpointRefreshPolicy
{
    Never,
    Always,
    InitOnly,
    Expired
}