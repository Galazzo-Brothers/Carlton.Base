namespace Carlton.Core.Components.Flux.Contracts;

public interface IExceptionDisplayService
{
    public ExceptionErrorPrompt GetExceptionErrorPrompt(Exception ex);
}


