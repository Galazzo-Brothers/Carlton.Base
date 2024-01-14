namespace Carlton.Core.Flux.Exceptions.ExceptionHandling;

public class FluxExceptionDisplayService : IFluxExceptionDisplayService
{
    public ErrorPromptModel GetErrorPromptModel(Exception ex, Action recoverAct)
    {
        return ex switch
        {
            FluxException fluxEx => new ErrorPromptModel
                            (
                                "Error",
                                fluxEx.Message,
                                "mdi-alert-circle-outline",
                                recoverAct
                            ),
            _ => new ErrorPromptModel
                            (
                               "Error",
                               "Oops! We are sorry an error has occurred. Please try again.",
                               "mdi-alert-circle-outline",
                               recoverAct
                            ),
        };
    }
}


