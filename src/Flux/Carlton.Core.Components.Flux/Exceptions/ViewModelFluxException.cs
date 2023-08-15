using Microsoft.JSInterop;
using System.Text.Json;

namespace Carlton.Core.Components.Flux;

public class ViewModelFluxException<TState, TViewModel> : Exception
{
    private const string ErrorMessage = $"An exception occurred during a ViewModelQuery of type {nameof(TViewModel)}";

    public int EventID { get; init; }
    public ViewModelQuery Query { get; init; }

    public ViewModelFluxException(ViewModelQuery query, Exception innerException)
       : this(ErrorMessage, LogEvents.ViewModel_Unhandled_Error, query, innerException)
    {
    }

    private ViewModelFluxException(string message, int eventID, ViewModelQuery query, Exception innerException) : base(message, innerException)
    {
        Query = query;
        EventID = eventID;
    }

    public static ViewModelFluxException<TState, TViewModel> ValidationError(ViewModelQuery query, ValidationException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_Validation_ErrorMsg, LogEvents.ViewModel_Validation_Error, query, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> JSInteropError(ViewModelQuery query, JSException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_JSInterop_ErrorMsg, LogEvents.ViewModel_JsInterop_Error, query, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> JsonError(ViewModelQuery query, JsonException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_JSON_ErrorMsg, LogEvents.ViewModel_JSON_Error, query, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> HttpError(ViewModelQuery query, HttpRequestException innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_HTTP_ErrorMsg, LogEvents.ViewModel_HTTP_Error, query, innerException);
    }

    public override string ToString()
    {
        return $"{ErrorMessage}" +
            $"{Environment.NewLine}" +
            $"ViewModelQueryID: {Query.QueryID}" +
            $"{base.ToString()}";
    }
}
