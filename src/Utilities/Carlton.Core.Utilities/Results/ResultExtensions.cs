namespace Carlton.Core.Utilities.Results;

public static class ResultExtensions
{
    public static Result<TValue, TError> SafeExecute<TValue, TError>(
        Func<Result<TValue, TError>> func,
        Func<TValue, TValue> success,
        Func<TError, TError> error,
        Func<Exception, TError> exception)
    {
        try
        {
            return func().Match<Result<TValue, TError>>
            (
                val => success(val),
                err => error(err)
            );
        }
        catch (Exception ex)
        {
            return exception(ex);
        }
    }

    public static async Task<Result<TValue, TError>> SafeExecuteAsync<TValue, TError>(
      Func<Task<Result<TValue, TError>>> func,
      Func<TValue, TValue> success,
      Func<TError, TError> error,
      Func<Exception, TError> exception)
    {
        try
        {
            return (await func()).Match<Result<TValue, TError>>
            (
                val => success(val),
                err => error(err)
            );
        }
        catch (Exception ex)
        {
            return exception(ex);
        }
    }

    public static Task<Result<TValue, TError>> ToResultTask<TValue, TError>(this TError error)
    {
        return Task.FromResult<Result<TValue, TError>>(error);
    }

    public static Result<TValue, TError> ToResult<TValue, TError>(this TError error)
    {
        return (Result<TValue, TError>)error;
    }
}
