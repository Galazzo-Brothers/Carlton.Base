namespace Carlton.Core.Flux.Contracts;

public record ErrorPromptModel(string Header, string Message, string IconClass, Action Recover);

public interface IFluxExceptionDisplayService
{
    public ErrorPromptModel GetErrorPromptModel(Exception ex, Action recoverAct);
}


