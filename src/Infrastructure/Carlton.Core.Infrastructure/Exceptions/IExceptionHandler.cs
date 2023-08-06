namespace Carlton.Core.Infrastructure.Exceptions;

public interface IExceptionHandler
{
    Task HandleException(Exception ex, object requestObject);
}
