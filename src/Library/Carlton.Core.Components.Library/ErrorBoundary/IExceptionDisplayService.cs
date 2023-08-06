namespace Carlton.Core.Components.Library;

public interface IExceptionDisplayService
{
    public ExceptionErrorPrompt GetExceptionErrorPrompt(Exception ex);
}

public record ExceptionErrorPrompt(string Header, string Message, string IconClass);
