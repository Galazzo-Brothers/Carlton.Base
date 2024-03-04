namespace Carlton.Core.Flux.Errors;

public static class FluxViewModelErrors
{
    public record ViewModelFluxError(string Message, int ErrorCode, Type ViewModelType);

    public record ValidationError(Type ViewModelType, IEnumerable<string> ValidationErrors)
        : ViewModelFluxError(new ViewModelFluxError($"{FluxLogs.ViewModel_Validation_ErrorMsg} {ViewModelType.GetDisplayName()}",
          FluxLogs.ViewModel_Validation_Error,
          ViewModelType));

    public record JsonError(Type ViewModelType)
        : ViewModelFluxError(new ViewModelFluxError($"{FluxLogs.ViewModel_JSON_ErrorMsg} {ViewModelType.GetDisplayName()}",
          FluxLogs.ViewModel_HTTP_Response_JSON_Error,
          ViewModelType));

    public record HttpUrlError(Type ViewModelType)
          : ViewModelFluxError(new ViewModelFluxError($"{FluxLogs.ViewModel_HTTP_URL_ErrorMsg} {ViewModelType.GetDisplayName()}",
            FluxLogs.ViewModel_HTTP_URL_Error,
            ViewModelType));

    public record HttpError(Type ViewModelType)
         : ViewModelFluxError(new ViewModelFluxError($"{FluxLogs.ViewModel_HTTP_URL_ErrorMsg} {ViewModelType.GetDisplayName()}",
           FluxLogs.ViewModel_HTTP_Request_Error,
           ViewModelType));


    public record JsInteropError(Type ViewModelType)
        : ViewModelFluxError(new ViewModelFluxError($"{FluxLogs.ViewModel_JSInterop_ErrorMsg} {ViewModelType.GetDisplayName()}",
          FluxLogs.ViewModel_JsInterop_Error,
          ViewModelType));

    public record UnhandledError(Type ViewModelType)
        : ViewModelFluxError(new ViewModelFluxError($"{FluxLogs.ViewModel_Unhandled_ErrorMsg} {ViewModelType.GetDisplayName()}",
          FluxLogs.ViewModel_Unhandled_Error,
          ViewModelType));
}
