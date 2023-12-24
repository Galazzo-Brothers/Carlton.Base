namespace Carlton.Core.Flux.Exceptions.ExceptionHandling;

public class FluxExceptionDisplayService : IFluxExceptionDisplayService
{
    public ExceptionErrorPrompt GetExceptionErrorPrompt(Exception ex, Action recoverAct)
    {
        return ex switch
        {
            FluxException fluxEx => new ExceptionErrorPrompt
                            (
                                "Error",
                                fluxEx.Message,
                                "mdi-alert-circle-outline",
                                recoverAct
                            ),
            _ => new ExceptionErrorPrompt
                            (
                               "Error",
                               "Oops! We are sorry an error has occurred. Please try again.",
                               "mdi-alert-circle-outline",
                               recoverAct
                            ),
        };
    }
}


