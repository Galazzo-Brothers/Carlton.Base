namespace Carlton.Core.Components.Flux;

public class ViewModelFluxException<TState, TViewModel> : Exception
{
    private const string ErrorMessage = $"An exception occurred during a request for ViewModel of type {nameof(TViewModel)}";

    public ViewModelQuery Query { get; init; }

    public ViewModelFluxException(ViewModelQuery query, Exception innerException)
       : this(ErrorMessage, query, innerException)
    {
    }

    private ViewModelFluxException(string message, ViewModelQuery query, Exception innerException) : base(message, innerException)
    {
        Query = query;
    }

    public static ViewModelFluxException<TState, TViewModel> ValidationError(ViewModelQuery query, Exception innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_Validation_ErrorMsg, query, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> JSInteropError(ViewModelQuery query, Exception innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_JSInterop_ErrorMsg, query, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> JsonError(ViewModelQuery query, Exception innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_JSON_ErrorMsg, query, innerException);
    }

    public static ViewModelFluxException<TState, TViewModel> HttpError(ViewModelQuery query, Exception innerException)
    {
        return new ViewModelFluxException<TState, TViewModel>(LogEvents.ViewModel_HTTP_ErrorMsg, query, innerException);
    }

    public override string ToString()
    {
        return $"{ErrorMessage}" +
            $"{Environment.NewLine}" +
            $"ViewModelQueryID: {Query.QueryID}" +
            $"{base.ToString()}";
    }
}
