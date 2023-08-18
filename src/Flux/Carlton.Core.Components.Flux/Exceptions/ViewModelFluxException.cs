using Carlton.Core.Components.Flux.Exceptions;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Carlton.Core.Components.Flux;

public class ViewModelFluxException<TState, TViewModel> : FluxException
{
    private const string ErrorMessage = $"An exception occurred during a ViewModelQuery of type {nameof(TViewModel)}";

    public ViewModelQuery Query { get; init; }

    public ViewModelFluxException(ViewModelQuery query, Exception innerException)
       : this(LogEvents.ViewModel_Unhandled_Error, ErrorMessage, query, innerException)
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

    public static ViewModelFluxException<TState, TViewModel> JSInteropError(ViewModelQuery query, JSException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_JsInterop_Error, LogEvents.ViewModel_JSInterop_ErrorMsg, query, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> JsonError(ViewModelQuery query, JsonException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_JSON_Error, LogEvents.ViewModel_JSON_ErrorMsg, query, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> HttpError(ViewModelQuery query, HttpRequestException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_HTTP_Error, LogEvents.ViewModel_HTTP_ErrorMsg, query, innerException);
    }

    public override string ToString()
    {
        return $"{ErrorMessage}" +
            $"{Environment.NewLine}" +
            $"ViewModelQueryID: {Query.QueryID}" +
            $"{base.ToString()}";
    }
}
