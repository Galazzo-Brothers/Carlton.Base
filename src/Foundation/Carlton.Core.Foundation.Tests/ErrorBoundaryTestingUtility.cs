using Microsoft.AspNetCore.Components;
using System.Reflection;
namespace Carlton.Core.Foundation.Tests;

public static class ErrorBoundaryTestingUtility
{
    public static void SimulateException(object instance, Exception exception)
    {
        var propertyInfo = typeof(ErrorBoundaryBase).GetProperty("CurrentException", BindingFlags.Instance | BindingFlags.NonPublic);
        var onErrorMethodInfo = instance.GetType().GetMethod("OnErrorAsync", BindingFlags.Instance | BindingFlags.NonPublic);

        propertyInfo.SetValue(instance, exception);
        onErrorMethodInfo.Invoke(instance, new object[] { exception });
    }
}
