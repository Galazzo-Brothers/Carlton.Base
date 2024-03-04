namespace Carlton.Core.Flux.Errors;

public static class ViewModelQueryErrors
{
    public record ViewModelQueryError(string Message, int ErrorCode, Type ViewModelType);

    public record ValidationError(Type ViewModelType, IEnumerable<string> ValidationErrors)
        : ViewModelQueryError(new ViewModelQueryError($"{FluxLogs.ViewModel_Validation_ErrorMsg} {ViewModelType.GetDisplayName()}",
          FluxLogs.ViewModel_Validation_Error,
          ViewModelType));

    public record JsonError(Type ViewModelType)
        : ViewModelQueryError(new ViewModelQueryError($"{FluxLogs.ViewModel_JSON_ErrorMsg} {ViewModelType.GetDisplayName()}",
          FluxLogs.ViewModel_HTTP_Response_JSON_Error,
          ViewModelType));

    public record HttpUrlError(Type ViewModelType)
          : ViewModelQueryError(new ViewModelQueryError($"{FluxLogs.ViewModel_HTTP_URL_ErrorMsg} {ViewModelType.GetDisplayName()}",
            FluxLogs.ViewModel_HTTP_URL_Error,
            ViewModelType));

    public record HttpError(Type ViewModelType)
         : ViewModelQueryError(new ViewModelQueryError($"{FluxLogs.ViewModel_HTTP_URL_ErrorMsg} {ViewModelType.GetDisplayName()}",
           FluxLogs.ViewModel_HTTP_Request_Error,
           ViewModelType));


    public record JsInteropError(Type ViewModelType)
        : ViewModelQueryError(new ViewModelQueryError($"{FluxLogs.ViewModel_JSInterop_ErrorMsg} {ViewModelType.GetDisplayName()}",
          FluxLogs.ViewModel_JsInterop_Error,
          ViewModelType));

    public record UnhandledError(Type ViewModelType)
        : ViewModelQueryError(new ViewModelQueryError($"{FluxLogs.ViewModel_Unhandled_ErrorMsg} {ViewModelType.GetDisplayName()}",
          FluxLogs.ViewModel_Unhandled_Error,
          ViewModelType));
}
