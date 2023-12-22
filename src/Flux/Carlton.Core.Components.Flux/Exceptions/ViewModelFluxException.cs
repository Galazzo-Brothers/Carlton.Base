using Carlton.Core.Components.Flux.Exceptions;
using System.Text.Json;

namespace Carlton.Core.Components.Flux;

public class ViewModelFluxException<TState, TViewModel> : FluxException
{
    public ViewModelQuery Query { get; init; }

    public ViewModelFluxException(ViewModelQuery query, Exception innerException)
       : this(LogEvents.ViewModel_Unhandled_Error, LogEvents.ViewModel_Unhandled_ErrorMsg, query, innerException)
    {
    }

    private ViewModelFluxException(int eventID, string message, ViewModelQuery query, Exception innerException) : base(eventID, message, innerException)
    {
        Query = query;
        EventID = eventID;
    }

    public static ViewModelFluxException<TState, TViewModel> ValidationError(ViewModelQuery query, ValidationException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_Validation_Error, LogEvents.ViewModel_Validation_ErrorMsg, query, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> MappingError(ViewModelQuery query, CompileException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_Mapping_Error, LogEvents.ViewModel_Mapping_ErrorMsg, query, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> JsonError(ViewModelQuery query, JsonException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_JSON_Error, LogEvents.ViewModel_JSON_ErrorMsg, query, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> JsonError(ViewModelQuery query, NotSupportedException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_JSON_Error, LogEvents.ViewModel_JSON_ErrorMsg, query, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> HttpError(ViewModelQuery query, HttpRequestException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_HTTP_Error, LogEvents.ViewModel_HTTP_ErrorMsg, query, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> HttpUrlError(ViewModelQuery query, InvalidOperationException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_HTTP_URL_Error, LogEvents.ViewModel_HTTP_URL_ErrorMsg, query, innerException);
    }

    public override string ToString()
    {
        return $"{Message}" +
            $"{Environment.NewLine}" +
            $"ViewModelQueryID: {Query.QueryTraceID}" +
            $"{base.ToString()}";
    }
}
