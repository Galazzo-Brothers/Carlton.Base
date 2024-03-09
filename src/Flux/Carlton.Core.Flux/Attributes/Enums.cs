namespace Carlton.Core.Flux.Attributes;


public enum HttpVerb
{
    GET,
    POST,
    PUT,
    PATCH,
    DELETE
}

public enum FluxServerCommunicationPolicy
{
    Never,
    Always
}