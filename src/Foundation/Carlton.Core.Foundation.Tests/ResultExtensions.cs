using Carlton.Core.Utilities.Results;
using System.Reflection;
namespace Carlton.Core.Foundation.Tests;

public static class ResultExtensions
{
    public static TValue GetValue<TValue, TError>(this Result<TValue, TError> result)
    {
        var fieldInfo = result.GetType().GetField("_value", BindingFlags.Instance | BindingFlags.NonPublic);
        return (TValue) fieldInfo.GetValue(result);
    }

    public static TError GetError<TValue, TError>(this Result<TValue, TError> result)
    {
        var fieldInfo = result.GetType().GetField("_error", BindingFlags.Instance | BindingFlags.NonPublic);
        return (TError)fieldInfo.GetValue(result);
    }
}
