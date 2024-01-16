using Microsoft.JSInterop;
namespace Carlton.Core.Flux.Exceptions;

public class ViewModelFluxException<TState, TViewModel> : FluxException
{
    private ViewModelFluxException(int eventId, string message, ViewModelQueryContext<TViewModel> context, Exception innerException) : base(eventId, context, message, innerException)
    {
        Context = context;
        EventId = eventId;
    }

    public static ViewModelFluxException<TState, TViewModel> ValidationError(ViewModelQueryContext<TViewModel> context, ValidationException innerException)
    {
        var message = $"{LogEvents.ViewModel_Validation_ErrorMsg} {context.ViewModelType}";
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_Validation_Error, message, context, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> JsonError(ViewModelQueryContext<TViewModel> context, JsonException innerException)
    {
        var message = $"{LogEvents.ViewModel_JSON_ErrorMsg} {context.ViewModelType}";
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_HTTP_Response_JSON_Error, message, context, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> JsonError(ViewModelQueryContext<TViewModel> context, NotSupportedException innerException)
    {
        var message = $"{LogEvents.ViewModel_JSON_ErrorMsg} {context.ViewModelType}";
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_HTTP_Response_JSON_Error, message, context, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> HttpError(ViewModelQueryContext<TViewModel> context, HttpRequestException innerException)
    {
        var message = $"{LogEvents.ViewModel_HTTP_ErrorMsg} {context.ViewModelType}";
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_HTTP_Request_Error, message, context, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> HttpUrlError(ViewModelQueryContext<TViewModel> context, InvalidOperationException innerException)
    {
        var message = $"{LogEvents.ViewModel_HTTP_URL_ErrorMsg} {context.ViewModelType}";
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_HTTP_Request_Error, message, context, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> JSInteropError(JSException innerException)
    {
        var message = $"{LogEvents.ViewModel_JSInterop_ErrorMsg} {typeof(TViewModel).GetDisplayName()}";
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_JsInterop_Error, message, null, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> UnhandledError(ViewModelQueryContext<TViewModel> context, Exception innerException)
    {
        var message = $"{LogEvents.ViewModel_Unhandled_ErrorMsg} {context.ViewModelType}";
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_Unhandled_Error, message, context, innerException);
    }

    public override string ToString()
    {
        return $"{Message}" +
            $"{Environment.NewLine}" +
            $"ViewModelQueryID: {Context.RequestID}" +
            $"{base.ToString()}";
    }
}
