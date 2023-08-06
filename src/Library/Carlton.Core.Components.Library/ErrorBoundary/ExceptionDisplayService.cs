namespace Carlton.Core.Components.Library;

public class ExceptionDisplayService : IExceptionDisplayService
{
    public ExceptionErrorPrompt GetExceptionErrorPrompt(Exception ex)
    {
        return new ExceptionErrorPrompt
        (
            "Error",
            "Oops! We are sorry an error has occurred. Please try again.",
            "mdi-alert-circle-outline"
        );
    }
}
