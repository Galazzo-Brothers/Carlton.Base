namespace Carlton.Core.Flux.Contracts;

public interface IFluxExceptionDisplayService
{
    public ExceptionErrorPrompt GetExceptionErrorPrompt(Exception ex, Action recoverAct);
}


