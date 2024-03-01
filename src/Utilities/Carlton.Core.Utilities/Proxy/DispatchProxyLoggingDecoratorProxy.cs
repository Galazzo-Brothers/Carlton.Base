using Microsoft.Extensions.Logging;
using System.Reflection;
namespace Carlton.Core.Utilities.Proxy;

/// <summary>
/// Decorates a target object with logging functionality using the DispatchProxy pattern.
/// </summary>
/// <typeparam name="TDecorated">The type of the object being decorated.</typeparam>
public class DispatchProxyLoggingDecorator<TDecorated> : DispatchProxy
  where TDecorated : class
{
    private ILogger _logger;

    /// <summary>
    /// Gets the target object being decorated.
    /// </summary>
    public TDecorated Target { get; private set; }

    /// <inheritdoc/>
    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        try
        {
            _logger.LogInformation("Calling method {TypeName}.{MethodName} with arguments {Arguments}",
                targetMethod.DeclaringType.Name, targetMethod.Name, args);

            var result = targetMethod.Invoke(Target, args);

            if(result is Task resultTask)
            {
                resultTask.ContinueWith(task =>
                {
                    if(task.IsFaulted)
                    {
                        _logger.LogError(task.Exception,
                            "An unhandled exception was raised during execution of {decoratedClass}.{methodName}",
                            typeof(TDecorated), targetMethod.Name);
                    }

                    _logger.LogInformation("Method {TypeName}.{MethodName} returned {ReturnValue}",
                        targetMethod.DeclaringType.Name, targetMethod.Name, result);
                });
            }
            else
            {
                _logger.LogInformation("Method {TypeName}.{MethodName} returned {ReturnValue}",
                    targetMethod.DeclaringType.Name, targetMethod.Name, result);
            }

            return result;
        }
        catch(TargetInvocationException ex)
        {
            _logger.LogError(ex.InnerException ?? ex,
                "Error during invocation of {decoratedClass}.{methodName}",
                typeof(TDecorated), targetMethod.Name);
            throw ex.InnerException ?? ex;
        }
    }

    /// <summary>
    /// Decorates the specified target object with logging functionality.
    /// </summary>
    /// <param name="target">The target object to decorate.</param>
    /// <param name="logger">The logger instance to use for logging.</param>
    /// <returns>The decorated object.</returns>
    public static TDecorated Decorate(TDecorated target, ILogger logger)
    {
        // DispatchProxy.Create creates proxy objects
        object proxy = Create<TDecorated, DispatchProxyLoggingDecorator<TDecorated>>();
        (proxy as DispatchProxyLoggingDecorator<TDecorated>).SetParameters(target, logger);

        return (TDecorated) proxy;
    }

    private void SetParameters(TDecorated target, ILogger logger)
    {
        Target = target;
        _logger = logger;
    }
}
