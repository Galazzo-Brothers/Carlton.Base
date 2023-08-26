namespace Carlton.Core.Components.Flux.Attributes;

public enum DataEndpointParameterType
{
    StateStoreParameter,
    ComponentParameter
}

public enum HttpVerb
{
    GET,
    POST,
    PUT,
    PATCH,
    DELETE
}

public enum DataEndpointRefreshPolicy
{
    Never,
    Always
}