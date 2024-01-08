using Microsoft.JSInterop;

namespace Carlton.Core.Flux.Exceptions;

public class ViewModelFluxException<TState, TViewModel> : FluxException
{
    public ViewModelQueryContext<TViewModel> Context { get; init; }

    public ViewModelFluxException(ViewModelQueryContext<TViewModel> query, Exception innerException)
       : this(LogEvents.ViewModel_Unhandled_Error, LogEvents.ViewModel_Unhandled_ErrorMsg, query, innerException)
    {
    }

    private ViewModelFluxException(int eventID, string message, ViewModelQueryContext<TViewModel> context, Exception innerException) : base(eventID, message, innerException)
    {
        Context = context;
        EventID = eventID;
    }

    public static ViewModelFluxException<TState, TViewModel> ValidationError(ViewModelQueryContext<TViewModel> context, ValidationException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_Validation_Error, LogEvents.ViewModel_Validation_ErrorMsg, context, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> MappingError(ViewModelQueryContext<TViewModel> context, CompileException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_Mapping_Error, LogEvents.ViewModel_Mapping_ErrorMsg, context, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> JsonError(ViewModelQueryContext<TViewModel> context, JsonException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_HTTP_Response_JSON_Error, LogEvents.ViewModel_JSON_ErrorMsg, context, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> JsonError(ViewModelQueryContext<TViewModel> context, NotSupportedException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_HTTP_Response_JSON_Error, LogEvents.ViewModel_JSON_ErrorMsg, context, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> HttpError(ViewModelQueryContext<TViewModel> context, HttpRequestException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_HTTP_Request_Error, LogEvents.ViewModel_HTTP_ErrorMsg, context, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> HttpUrlError(ViewModelQueryContext<TViewModel> context, InvalidOperationException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_HTTP_Request_Error, LogEvents.ViewModel_HTTP_URL_ErrorMsg, context, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> JSInteropError(JSException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_JsInterop_Error, LogEvents.ViewModel_JSInterop_ErrorMsg, new ViewModelQueryContext<TViewModel>(), innerException);
    }

    public override string ToString()
    {
        return $"{Message}" +
            $"{Environment.NewLine}" +
            $"ViewModelQueryID: {Context.RequestID}" +
            $"{base.ToString()}";
    }
}
