using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Dispatchers;
using System.Net;
namespace Carlton.Core.Flux.Errors;

public static class FluxErrors
{
    public abstract record FluxError(string Message, int EventId, BaseRequestContext Context);

    public sealed record UnsupportedHttpVerbError(HttpVerb HttpVerb, BaseRequestContext Context) 
        : FluxError($"{HttpVerb} is not a supported operation.", 1, Context);

    public sealed record ValidationError(IEnumerable<string> ValidationErrors, BaseRequestContext Context) 
        : FluxError($"{FluxLogs.ViewModel_HTTP_URL_ErrorMsg} {Context.FluxOperationTypeName}", FluxLogs.Mutation_Validation_Error, Context);

    public sealed record HttpError(string HttpVerb,
        HttpStatusCode? HttpStatusCode,
        HttpResponseMessage HttpResponse,
        BaseRequestContext Context) 
        : FluxError($"{FluxLogs.ViewModel_JSInterop_ErrorMsg} {Context.FluxOperationTypeName}", FluxLogs.Mutation_HttpInterception_Request_Error, Context);
    
    public sealed record HttpRequestError(HttpRequestException Exception, BaseRequestContext Context)
        : FluxError($"An HttpRequest exception occurred while {Context.FluxOperationTypeName}", FluxLogs.Mutation_HttpInterception_Request_Error, Context);

    public sealed record UnhandledFluxError(Exception Exception, BaseRequestContext Context) 
        : FluxError($"{FluxLogs.ViewModel_Unhandled_ErrorMsg} {Context.FluxOperationTypeName}", FluxLogs.Mutation_Unhandled_Error, Context);

    public sealed record JsonError(Exception Exception, BaseRequestContext Context)
        : FluxError($"{FluxLogs.ViewModel_JSON_ErrorMsg} {Context.FluxOperationTypeName}", FluxLogs.Mutation_SaveLocalStorage_JSON_Error, Context);
 
    public sealed record HttpUrlConstructionError(string Url, BaseRequestContext Context) 
        : FluxError($"{FluxLogs.ViewModel_HTTP_URL_ErrorMsg} {Context.FluxOperationTypeName}", FluxLogs.Mutation_HttpInterception_UrlConstruction_Error, Context);
   
}
