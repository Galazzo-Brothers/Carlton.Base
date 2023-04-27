namespace Carlton.Base.State;

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